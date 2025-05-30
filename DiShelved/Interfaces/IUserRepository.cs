using DiShelved.Models;

namespace DiShelved.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User User);
    
  }
}
