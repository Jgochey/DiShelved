using DiShelved.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DiShelved.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User User);

        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUidAsync(string uid);

    }
}
