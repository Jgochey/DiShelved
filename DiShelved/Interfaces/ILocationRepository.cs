using DiShelved.Models;

namespace DiShelved.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetLocationsByUserIdAsync(int userId);
        Task<IEnumerable<Location>> GetLocationsByUserUidAsync(string Uid);
        Task<Location> GetLocationByIdAsync(int id);
        Task<Location> CreateLocationAsync(Location Location);
        Task<Location> UpdateLocationAsync(int id, Location Location);
        Task<bool> DeleteLocationAsync(int id);
    }
}
