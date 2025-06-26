using Microsoft.EntityFrameworkCore;
using DiShelved.Models;
using DiShelved.Interfaces;
using DiShelved.Data;

namespace DiShelved.Repositories
{
    public class ContainerRepository : IContainerRepository
    {
        private readonly DiShelvedDbContext _context;
        public ContainerRepository(DiShelvedDbContext context) => _context = context;

        public async Task<IEnumerable<Container>> GetContainersByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                return (IEnumerable<Container>)Results.BadRequest("User Id not found");
            }
            var containers = await _context.Containers
                .Where(c => c.UserId == userId)
                .ToListAsync();
            if (containers == null || !containers.Any())
            {
                return (IEnumerable<Container>)Results.BadRequest("Containers not found for this User Id");
            }
            return containers;
        }
        public async Task<Container> GetContainerByIdAsync(int id)
        {
            if (id <= 0)
            {
                return (Container)Results.BadRequest("Container Id not found");
            }
            var Container = await _context.Containers.FindAsync(id);
            if (Container == null)
            {
                return (Container)Results.BadRequest("Container not found");
            }
            return Container;
        }
        public async Task<Container> CreateContainerAsync(Container Container)
        {
            _context.Containers.Add(Container);
            await _context.SaveChangesAsync();
            return Container;
        }
        public async Task<Container> UpdateContainerAsync(int id, Container Container)
        {
            var existingContainer = await _context.Containers.FindAsync(id);
            if (existingContainer == null)
            {
                return (Container)Results.BadRequest("Container not found");
            }

            existingContainer.Name = Container.Name;
            existingContainer.Description = Container.Description;
            existingContainer.LocationId = Container.LocationId;
            existingContainer.UserId = Container.UserId;
            existingContainer.Image = Container.Image;

            await _context.SaveChangesAsync();
            return existingContainer;
        }

        public async Task<bool> DeleteContainerAsync(int id)
        {
            var Container = await _context.Containers.FindAsync(id);
            if (Container == null)
            {
                return false;
            }
            _context.Containers.Remove(Container);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<Container>> GetContainersByLocationIdAsync(int locationId)
        {
            if (locationId <= 0)
            {
                return Task.FromResult<IEnumerable<Container>>(new List<Container>());
            }
            var containers = _context.Containers.Where(c => c.LocationId == locationId);
            return Task.FromResult<IEnumerable<Container>>(containers.ToList());
        }
    }
}
