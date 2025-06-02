using DiShelved.Services;
using DiShelved.Models;
using DiShelved.Interfaces;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace DiShelved.Tests
{

  public class LocationServiceTests

  {
    private readonly LocationService _locationService;

    private readonly Mock<ILocationRepository> _mockLocationRepository;

    public LocationServiceTests()
    {
      _mockLocationRepository = new Mock<ILocationRepository>();
      _locationService = new LocationService(_mockLocationRepository.Object);
    }


    [Fact]
    public async Task GetLocationById_ShouldReturnLocation_WhenLocationExists()
    {
      var LocationId = 1;

      var expectedLocation = new Location { Id = LocationId, Name = "Garage", Description = "The shelf inside the Garage.", UserId = 1 };

      // The GetLocationById method is called with the LocationId parameter, the mock object should return the expectedLocation instance.
      _mockLocationRepository.Setup(repo => repo.GetLocationByIdAsync(LocationId)).Returns(Task.FromResult(expectedLocation));

      var actualLocation = await _locationService.GetLocationByIdAsync(LocationId);

      // The actualLocation returned by the GetLocationById method should be equal to the expectedLocation instance.
      Assert.Equal(expectedLocation, actualLocation);
    }

    [Fact]
    public async Task GetLocationById_ShouldReturnNull_WhenLocationDoesNotExist()
    {
      // A Location with an Id of 999 does not exist in the repository
      // Therefore, the GetLocationById method should return null.
      var LocationId = 999;

      _mockLocationRepository.Setup(repo => repo.GetLocationByIdAsync(LocationId)).Returns(Task.FromResult<Location>(null!));

      var actualLocation = await _locationService.GetLocationByIdAsync(LocationId);

      // The actualLocation returned should be null.
      Assert.Null(actualLocation);
    }

    [Fact]
    public async Task GetLocationByUid_ShouldReturnLocation_WhenLocationExists()
    {

      var userUid = "test-uid";
      var expectedLocation = new Location { Id = 1, Name = "Garage", Description = "The shelf inside the Garage.", UserId = 1 };

      // The GetLocationsByUserUidAsync method is called with the userUid parameter, the mock object should return a list containing the expectedLocation instance.
      _mockLocationRepository.Setup(repo => repo.GetLocationsByUserUidAsync(userUid))
      .Returns(Task.FromResult<IEnumerable<Location>>(new List<Location> { expectedLocation }));

      var actualLocations = await _locationService.GetLocationsByUserUidAsync(userUid);

      // The actualLocations returned by the GetLocationsByUserUidAsync method should contain the expectedLocation instance.
      Assert.Single(actualLocations);
      Assert.Equal(expectedLocation, actualLocations.First());
    }

    [Fact]
    public async Task GetLocationsByUserUid_ShouldReturnEmptyList_WhenNoLocationsExist()
    {
      var userUid = "non-existent-uid";

      _mockLocationRepository.Setup(repo => repo.GetLocationsByUserUidAsync(userUid))
      .Returns(Task.FromResult<IEnumerable<Location>>(new List<Location>()));

      var actualLocations = await _locationService.GetLocationsByUserUidAsync(userUid);

      Assert.Empty(actualLocations);
    }

    [Fact]
    public async Task CreateLocation_ShouldCreateLocation_WhenLocationIsValid()
    {
      var newLocation = new Location { Id = 1, Name = "Garage", Description = "The shelf inside the Garage.", UserId = 1 };
      // The CreateLocation method should return the newLocation instance when the newLocation parameter is valid.

      _mockLocationRepository.Setup(repo => repo.CreateLocationAsync(newLocation)).Verifiable();

      // The Verify method is used to verify that the CreateLocation method was called with the newLocation parameter.
      await _mockLocationRepository.Object.CreateLocationAsync(newLocation);
      _mockLocationRepository.Verify(repo => repo.CreateLocationAsync(newLocation), Times.Once);
    }

    [Fact]
    public async Task CreateLocation_ShouldThrowException_WhenLocationIsNull()
    {
      Location newLocation = null;

      // The CreateLocation method should throw an ArgumentNullException when the newLocation parameter is null.
      await Assert.ThrowsAsync<ArgumentNullException>(async () => await _locationService.CreateLocationAsync(newLocation));
    }

    [Fact]
    public async Task UpdateLocation_ShouldUpdateLocation_WhenLocationExists()
    {
      var existingLocation = new Location { Id = 1, Name = "Garage", Description = "The shelf inside the Garage.", UserId = 1 };
      var updatedLocation = new Location { Id = 200, Name = "Storage Unit", Description = "123 Example Lane", UserId = 1 };
      // The UpdateLocation method should return the updatedLocation instance when the updatedLocation parameter is valid.

      _mockLocationRepository.Setup(repo => repo.GetLocationByIdAsync(existingLocation.Id)).Returns(Task.FromResult(existingLocation));
      _mockLocationRepository.Setup(repo => repo.UpdateLocationAsync(updatedLocation.Id, updatedLocation)).Returns(Task.FromResult(updatedLocation)).Verifiable();

      await _locationService.UpdateLocationAsync(updatedLocation.Id, updatedLocation);

      _mockLocationRepository.Verify(repo => repo.UpdateLocationAsync(updatedLocation.Id, updatedLocation), Times.Once);
    }

    [Fact]
    public async Task UpdateLocation_ShouldThrowException_WhenLocationDoesNotExist()
    {
      var nonExistingLocation = new Location { Id = 400, Name = "Nowhere", Description = "Somewhere that is not.", UserId = 1 };

      _mockLocationRepository.Setup(repo => repo.GetLocationByIdAsync(nonExistingLocation.Id)).Returns(Task.FromResult<Location>(null));

      await Assert.ThrowsAsync<InvalidOperationException>(async () => await _locationService.UpdateLocationAsync(nonExistingLocation.Id, nonExistingLocation));
    }

    [Fact]
    public async Task DeleteLocation_ShouldDeleteLocation_WhenLocationExists()
    {
      var LocationId = 1;
      var existingLocation = new Location { Id = LocationId, Name = "Garage", Description = "The shelf inside the Garage.", UserId = 1 };

      _mockLocationRepository.Setup(repo => repo.GetLocationByIdAsync(LocationId)).Returns(Task.FromResult(existingLocation));
      _mockLocationRepository.Setup(repo => repo.DeleteLocationAsync(LocationId))
          .Returns(Task.FromResult(true)).Verifiable();

      await _locationService.DeleteLocationAsync(LocationId);

      _mockLocationRepository.Verify(repo => repo.DeleteLocationAsync(LocationId), Times.Once);
    }

    [Fact]
    public async Task DeleteLocation_ShouldThrowException_WhenLocationDoesNotExist()
    {
      var LocationId = 999;

      _mockLocationRepository.Setup(repo => repo.GetLocationByIdAsync(LocationId)).Returns(Task.FromResult<Location>(null));

      await Assert.ThrowsAsync<InvalidOperationException>(async () => await _locationService.DeleteLocationAsync(LocationId));
    }
  }
}
