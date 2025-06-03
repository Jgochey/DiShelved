using DiShelved.Services;
using DiShelved.Models;
using DiShelved.Interfaces;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace DiShelved.Tests
{

  public class ContainerServiceTests

  {
    private readonly ContainerService _containerService;

    private readonly Mock<IContainerRepository> _mockContainerRepository;

    public ContainerServiceTests()
    {
      _mockContainerRepository = new Mock<IContainerRepository>();
      _containerService = new ContainerService(_mockContainerRepository.Object);
    }


    [Fact]
    public async Task GetContainerById_ShouldReturnContainer_WhenContainerExists()
    {
      var ContainerId = 1;

      var expectedContainer = new Container { Id = ContainerId, Name = "Box", Description = "of stuff", LocationId = 1, UserId = 1, Image = "https://example.com/image.jpg" };

      // The GetContainerById method is called with the ContainerId parameter, the mock object should return the expectedContainer instance.
      _mockContainerRepository.Setup(repo => repo.GetContainerByIdAsync(ContainerId)).Returns(Task.FromResult(expectedContainer));

      var actualContainer = await _containerService.GetContainerByIdAsync(ContainerId);

      // The actualContainer returned by the GetContainerById method should be equal to the expectedContainer instance.
      Assert.Equal(expectedContainer, actualContainer);
    }

    [Fact]
    public async Task GetContainerById_ShouldReturnNull_WhenContainerDoesNotExist()
    {
      // A Container with an Id of 999 does not exist in the repository
      // Therefore, the GetContainerById method should return null.
      var ContainerId = 999;

      _mockContainerRepository.Setup(repo => repo.GetContainerByIdAsync(ContainerId)).Returns(Task.FromResult<Container>(null!));

      var actualContainer = await _containerService.GetContainerByIdAsync(ContainerId);

      // The actualContainer returned should be null.
      Assert.Null(actualContainer);
    }

    [Fact]
    public async Task CreateContainer_ShouldCreateContainer_WhenContainerIsValid()
    {
      var newContainer = new Container { Id = 88, Name = "Box", Description = "of stuff", LocationId = 1, UserId = 1, Image = "https://example.com/image.jpg" };
      // The CreateContainer method should return the newContainer instance when the newContainer parameter is valid.

      _mockContainerRepository.Setup(repo => repo.CreateContainerAsync(newContainer)).Verifiable();

      // The Verify method is used to verify that the CreateContainer method was called with the newContainer parameter.
      await _mockContainerRepository.Object.CreateContainerAsync(newContainer);
      _mockContainerRepository.Verify(repo => repo.CreateContainerAsync(newContainer), Times.Once);
    }

    [Fact]
    public async Task CreateContainer_ShouldThrowException_WhenContainerIsNull()
    {
      Container newContainer = null;

      // The CreateContainer method should throw an ArgumentNullException when the newContainer parameter is null.
      await Assert.ThrowsAsync<ArgumentNullException>(async () => await _containerService.CreateContainerAsync(newContainer));
    }

    [Fact]
    public async Task UpdateContainer_ShouldUpdateContainer_WhenContainerExists()
    {
      var existingContainer = new Container { Id = 88, Name = "Box", Description = "of stuff", LocationId = 1, UserId = 1, Image = "https://example.com/image.jpg" };
      var updatedContainer = new Container { Id = 89, Name = "Jar", Description = "of things", LocationId = 1, UserId = 1, Image = "https://example.com/image.jpg" };
      // The UpdateContainer method should return the updatedContainer instance when the updatedContainer parameter is valid.

      _mockContainerRepository.Setup(repo => repo.GetContainerByIdAsync(existingContainer.Id)).Returns(Task.FromResult(existingContainer));
      _mockContainerRepository.Setup(repo => repo.UpdateContainerAsync(updatedContainer.Id, updatedContainer)).Returns(Task.FromResult(updatedContainer)).Verifiable();

      await _containerService.UpdateContainerAsync(updatedContainer.Id, updatedContainer);

      _mockContainerRepository.Verify(repo => repo.UpdateContainerAsync(updatedContainer.Id, updatedContainer), Times.Once);
    }

    [Fact]
    public async Task UpdateContainer_ShouldThrowException_WhenContainerDoesNotExist()
    {
      var nonExistingContainer = new Container { Id = 99999, Name = "Nothing", Description = "of nothing", LocationId = 1, UserId = 1, Image = "https://example.com/image.jpg" };

      _mockContainerRepository.Setup(repo => repo.GetContainerByIdAsync(nonExistingContainer.Id)).Returns(Task.FromResult<Container>(null));

      await Assert.ThrowsAsync<InvalidOperationException>(async () => await _containerService.UpdateContainerAsync(nonExistingContainer.Id, nonExistingContainer));
    }

    [Fact]
    public async Task DeleteContainer_ShouldDeleteContainer_WhenContainerExists()
    {
      var ContainerId = 1;
      var existingContainer = new Container { Id = ContainerId, Name = "Box", Description = "of stuff", LocationId = 1, UserId = 1, Image = "https://example.com/image.jpg" };

      _mockContainerRepository.Setup(repo => repo.GetContainerByIdAsync(ContainerId)).Returns(Task.FromResult(existingContainer));
      _mockContainerRepository.Setup(repo => repo.DeleteContainerAsync(ContainerId))
          .Returns(Task.FromResult(true)).Verifiable();

      await _containerService.DeleteContainerAsync(ContainerId);

      _mockContainerRepository.Verify(repo => repo.DeleteContainerAsync(ContainerId), Times.Once);
    }

    [Fact]
    public async Task DeleteContainer_ShouldThrowException_WhenContainerDoesNotExist()
    {
      var ContainerId = 999;

      _mockContainerRepository.Setup(repo => repo.GetContainerByIdAsync(ContainerId)).Returns(Task.FromResult<Container>(null));

      await Assert.ThrowsAsync<InvalidOperationException>(async () => await _containerService.DeleteContainerAsync(ContainerId));
    }
  }
}
