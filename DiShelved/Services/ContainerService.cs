using DiShelved.Models;
using DiShelved.Interfaces;
using System.Net;

namespace DiShelved.Services
{
    public class ContainerService : IContainerService
    {
        private readonly IContainerRepository _ContainerRepository;
        public ContainerService(IContainerRepository ContainerRepository) => _ContainerRepository = ContainerRepository;

        public async Task<Container?> GetContainerByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Container Id", nameof(id));
            }
            var Container = await _ContainerRepository.GetContainerByIdAsync(id);
            if (Container == null)
            {
                return null;
            }
            return Container;
        }
        public async Task<IEnumerable<Container>> GetAllContainersAsync()
        {
            var Containers = await _ContainerRepository.GetAllContainersAsync();
            if (Containers == null || !Containers.Any())
            {
                throw new InvalidOperationException("No Containers Found");
            }
            return Containers;
        }
        public async Task<Container> CreateContainerAsync(Container Container)
        {
            if (Container == null)
            {
                throw new ArgumentNullException(nameof(Container), "Created Container cannot be null");
            }
            var createdContainer = await _ContainerRepository.CreateContainerAsync(Container);
            if (createdContainer == null)
            {
                return (Container)Results.BadRequest("Container Not Created");
            }
            return createdContainer;
        }

        public async Task<Container> UpdateContainerAsync(int id, Container Container)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Container Id", nameof(id));
            }
            if (Container == null || Container.Id <= 0)
            {
                throw new ArgumentNullException("Invalid Container data", nameof(Container));
            }
            var updatedContainer = await _ContainerRepository.UpdateContainerAsync(Container.Id, Container);
            if (updatedContainer == null)
            {
                throw new InvalidOperationException("Container could not be updated");
            }
            return updatedContainer;
        }
        public async Task<bool> DeleteContainerAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Container Id");
            }

            var deleted = await _ContainerRepository.DeleteContainerAsync(id);
            if (!deleted)
            {
                throw new InvalidOperationException("Container could not be deleted");
            }

            return deleted;
        }

        public async Task<IEnumerable<Container>> GetContainersByLocationIdAsync(int locationId)
        {
            if (locationId <= 0)
            {
                throw new ArgumentException("Invalid Location Id", nameof(locationId));
            }
            var containers = await _ContainerRepository.GetContainersByLocationIdAsync(locationId);
            // If no containers are found, return an empty list instead of null.
            return containers ?? Enumerable.Empty<Container>();
        }
    }
}
