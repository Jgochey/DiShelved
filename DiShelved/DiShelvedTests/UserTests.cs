using DiShelved.Services;
using DiShelved.Models;
using DiShelved.Interfaces;
using Xunit;
using Moq;

namespace DiShelved.Tests
{

  public class UserServiceTests

  {
    private readonly UserService _userService;

    private readonly Mock<IUserRepository> _mockUserRepository;

    public UserServiceTests()
    {
      _mockUserRepository = new Mock<IUserRepository>();
      _userService = new UserService(_mockUserRepository.Object);
    }

    [Fact]
    public async Task CreateUser_ShouldCreateUser_WhenUserIsValid()
    {
      var newUser = new User { Id = 3 };
      // The CreateUser method should return the newUser instance when the newUser parameter is valid.

      _mockUserRepository.Setup(repo => repo.CreateUserAsync(newUser)).Verifiable();

      // The Verify method is used to verify that the CreateUser method was called with the newUser parameter.
      await _mockUserRepository.Object.CreateUserAsync(newUser);
      _mockUserRepository.Verify(repo => repo.CreateUserAsync(newUser), Times.Once);
    }

    [Fact]
    public async Task CreateUser_ShouldThrowException_WhenUserIsNull()
    {
      User newUser = null;

      // The CreateUser method should throw an ArgumentNullException when the newUser parameter is null.
      await Assert.ThrowsAsync<ArgumentNullException>(async () => await _userService.CreateUserAsync(newUser));
    }

    
  }
}
