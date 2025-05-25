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







        // Testing purposes, remove later.       
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null)
            {
                return false;
            }
            _context.Users.Remove(User);
            await _context.SaveChangesAsync();
            return true;
        }
  }
}
