using Microsoft.EntityFrameworkCore;
using DiShelved.Models;
using DiShelved.Interfaces;
using DiShelved.Data;

namespace DiShelved.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DiShelvedDbContext _context;
        public LocationRepository(DiShelvedDbContext context) => _context = context;
       
        public async Task<IEnumerable<Location>> GetLocationsByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                return (IEnumerable<Location>)Results.BadRequest("User Id not found");
            }
            var locations = await _context.Locations
                .Where(l => l.UserId == userId)
                .ToListAsync();
            if (locations == null || !locations.Any())
            {
                return (IEnumerable<Location>)Results.BadRequest("Locations not found for this User Id");
            }
            return locations;
        }

        public async Task<IEnumerable<Location>?> GetLocationsByUserUidAsync(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                return null;
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Uid == uid);
            if (user == null)
            {
                return null;
            }

            var locations = await _context.Locations
                .Where(l => l.UserId == user.Id)
                .ToListAsync();

            return locations;
        }

        public async Task<Location> GetLocationByIdAsync(int id)
        {
            if (id <= 0)
            {
                return (Location)Results.BadRequest("Location Id not found");
            }
            var Location = await _context.Locations.FindAsync(id);
            if (Location == null)
            {
                return (Location)Results.BadRequest("Location not found");
            }
            return Location;
        }
        public async Task<Location> CreateLocationAsync(Location Location)
        {
            _context.Locations.Add(Location);
            await _context.SaveChangesAsync();
            return Location;
        }
        public async Task<Location> UpdateLocationAsync(int id, Location Location)
        {
            var existingLocation = await _context.Locations.FindAsync(id);
            if (existingLocation == null)
            {
                return (Location)Results.BadRequest("Location not found");
            }

            existingLocation.Name = Location.Name;
            existingLocation.Description = Location.Description;
            existingLocation.UserId = Location.UserId;

            await _context.SaveChangesAsync();
            return existingLocation;
        }

        public async Task<bool> DeleteLocationAsync(int id)
        {
            var Location = await _context.Locations.FindAsync(id);
            if (Location == null)
            {
                return false;
            }
            _context.Locations.Remove(Location);
            await _context.SaveChangesAsync();
            return true;
        }
  }
}
