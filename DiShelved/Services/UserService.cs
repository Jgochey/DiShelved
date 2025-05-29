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

    }
}
