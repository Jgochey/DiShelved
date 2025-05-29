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
            var createdUser = await _userRepository.CreateUserAsync(User);
            if (createdUser == null)
            {
                return (User)Results.BadRequest("User Not Created");
            }
            return createdUser;
        }

    // public async Task<IEnumerable<User>> GetAllUsersAsync()
    //     {
    //         var users = await _userRepository.GetAllUsersAsync();
    //         if (users == null || !users.Any())
    //         {
    //             throw new InvalidOperationException("No Users Found");
    //         }
    //         return users;
    //     }

    }
}
