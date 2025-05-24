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
       
        public async Task<IEnumerable<Container>> GetAllContainersAsync()
        {
            return await _context.Containers.ToListAsync();
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
  }
}
