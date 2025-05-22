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
       
        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _context.Locations.ToListAsync();
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
