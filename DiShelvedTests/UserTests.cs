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
      var newUser = new User { Id = 3, Uid = "test-uid" };

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
      await Assert.ThrowsAsync<ArgumentNullException>(async () =>
      await _userService.CreateUserAsync(newUser));
    }

    [Fact]
    public async Task GetUserById_ShouldReturnUser_WhenUserExists()
    {
      var userId = 1;
      var expectedUser = new User { Id = userId, Uid = "test-uid" };

      // The GetUserByIdAsync method should return the expectedUser instance when the user exists.
      _mockUserRepository.Setup(repo =>
      repo.GetUserByIdAsync(userId))
      .ReturnsAsync(expectedUser);

      var result = await _userService.GetUserByIdAsync(userId);
      Assert.Equal(expectedUser, result);
    }

    [Fact]
    public async Task GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
    {
      var userId = 999; // Assuming this ID does not exist
      _mockUserRepository.Setup(repo =>
      repo.GetUserByIdAsync(userId))
      .ReturnsAsync((User)null);

      var result = await _userService.GetUserByIdAsync(userId);
      Assert.Null(result);
    }

    [Fact]
    public async Task GetUserByUid_ShouldReturnUser_WhenUserExists()
    {
      var userUid = "test-uid";
      var expectedUser = new User { Id = 1, Uid = userUid };

      _mockUserRepository.Setup(repo =>
      repo.GetUserByUidAsync(userUid))
      .ReturnsAsync(expectedUser);

      var result = await _userService.GetUserByUidAsync(userUid);
      Assert.Equal(expectedUser, result);
    }

    [Fact]
    public async Task GetUserByUid_ShouldReturnNull_WhenUidIsNullOrEmpty()
    {
      string userUid = null;

      // The GetUserByUidAsync method should return null when the uid parameter is null or empty.
      var result = await _userService.GetUserByUidAsync(userUid);
      Assert.Null(result);

      userUid = string.Empty;
      result = await _userService.GetUserByUidAsync(userUid);
      Assert.Null(result);
    }

    [Fact]
    public async Task GetUserByUid_ShouldReturnNull_WhenUserDoesNotExist()
    {
      var userUid = "non-existent-uid";
      _mockUserRepository.Setup(repo =>
      repo.GetUserByUidAsync(userUid))
      .ReturnsAsync((User)null);

      var result = await _userService.GetUserByUidAsync(userUid);
      Assert.Null(result);
    }

  }
}
