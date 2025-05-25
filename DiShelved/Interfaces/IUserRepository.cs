using DiShelved.Models;

namespace DiShelved.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User User);


        // Testing purposes, remove later.
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int id);
    }
}
