using DiShelved.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DiShelved.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User User);

        // Testing purposes, remove later.
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int id);
    }
}
