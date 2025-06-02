using DiShelved.Models;
using DiShelved.Interfaces;

namespace DiShelved.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository UserRepository) => _userRepository = UserRepository;

        public async Task<User> CreateUserAsync(User User)
        {
            if (User == null)
            {
                throw new ArgumentNullException(nameof(User), "Created User cannot be null");
            }

            // Check for existing user by UID
            var existingUser = await _userRepository.GetUserByUidAsync(User.Uid);
            if (existingUser != null)
            {
                // Return the existing user if it already exists
                return existingUser;

            }

            var createdUser = await _userRepository.CreateUserAsync(User);
            if (createdUser == null)
            {
                throw new InvalidOperationException("User Not Created");
            }
            return createdUser;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User?> GetUserByUidAsync(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                return null;
            }
            return await _userRepository.GetUserByUidAsync(uid);
        }
    }
}
