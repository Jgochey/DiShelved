using DiShelved.Models;
using DiShelved.Interfaces;

namespace DiShelved.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _LocationRepository;
        public LocationService(ILocationRepository LocationRepository) => _LocationRepository = LocationRepository;

        public async Task<Location?> GetLocationByIdAsync(int id)
        {
            if (id <= 0)
            {
            throw new ArgumentException("Invalid Location Id", nameof(id));
            }
            var Location = await _LocationRepository.GetLocationByIdAsync(id);
            if (Location == null)
            {
                return null;
            }
            return Location;
        }
        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            var Locations = await _LocationRepository.GetAllLocationsAsync();
            if (Locations == null || !Locations.Any())
            {
                throw new InvalidOperationException("No Locations Found");
            }
            return Locations;
        }
        public async Task<Location> CreateLocationAsync(Location Location)
        {
            if (Location == null)
            {
                throw new ArgumentNullException(nameof(Location), "Created Location cannot be null");
            }
            var createdLocation = await _LocationRepository.CreateLocationAsync(Location);
            if (createdLocation == null)
            {
                return (Location)Results.BadRequest("Location Not Created");
            }
            return createdLocation;
        }

        public async Task<Location> UpdateLocationAsync(int id, Location Location)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Location Id", nameof(id));
            }
            if (Location == null || Location.Id <= 0)
            {
                throw new ArgumentNullException("Invalid Location data", nameof(Location));
            }
            var updatedLocation = await _LocationRepository.UpdateLocationAsync(Location.Id, Location);
            if (updatedLocation == null)
            {
                throw new InvalidOperationException("Location could not be updated");
            }
            return updatedLocation;
        }
        public async Task<bool> DeleteLocationAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Location Id");
            }

            var deleted = await _LocationRepository.DeleteLocationAsync(id);
            if (!deleted)
            {
                throw new InvalidOperationException("Location could not be deleted");
            }

            return deleted;
        }
    }
}
