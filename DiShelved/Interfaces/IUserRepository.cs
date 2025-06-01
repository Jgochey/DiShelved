using DiShelved.Models;

namespace DiShelved.Interfaces
{
  public interface IUserRepository
  {
    Task<User> CreateUserAsync(User User);

    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUidAsync(string uid);

    Task<IEnumerable<User>> GetAllUsersAsync();
    
  }
}
