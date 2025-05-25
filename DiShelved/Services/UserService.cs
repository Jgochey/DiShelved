using DiShelved.Models;
using DiShelved.Interfaces;

namespace DiShelved.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        public UserService(IUserRepository UserRepository) => _UserRepository = UserRepository;

        public async Task<User> CreateUserAsync(User User)
        {
            if (User == null)
            {
                throw new ArgumentNullException(nameof(User), "Created User cannot be null");
            }
            var createdUser = await _UserRepository.CreateUserAsync(User);
            if (createdUser == null)
            {
                return (User)Results.BadRequest("User Not Created");
            }
            return createdUser;
        }





        // Testing purposes, remove later.
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var Users = await _UserRepository.GetAllUsersAsync();
            if (Users == null || !Users.Any())
            {
                throw new InvalidOperationException("No Users Found");
            }
            return Users;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid User Id");
            }

            var deleted = await _UserRepository.DeleteUserAsync(id);
            if (!deleted)
            {
                throw new InvalidOperationException("User could not be deleted");
            }

            return deleted;
        }
    }
}
