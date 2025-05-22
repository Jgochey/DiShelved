using DiShelved.Models;

namespace DiShelved.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllLocationsAsync();
        Task<Location> GetLocationByIdAsync(int id);
        Task<Location> CreateLocationAsync(Location Location);
        Task<Location> UpdateLocationAsync(int id, Location Location);
        Task<bool> DeleteLocationAsync(int id);
    }
}
