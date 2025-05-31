using Microsoft.EntityFrameworkCore;
using DiShelved.Models;
using DiShelved.Interfaces;
using DiShelved.Data;

namespace DiShelved.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DiShelvedDbContext _context;
        public UserRepository(DiShelvedDbContext context) => _context = context;
        public async Task<User> CreateUserAsync(User User)
        {
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return User;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

  }
}
