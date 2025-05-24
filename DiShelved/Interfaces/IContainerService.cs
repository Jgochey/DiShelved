using DiShelved.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DiShelved.Interfaces
{
    public interface IContainerService
    {
        Task<IEnumerable<Container>> GetAllContainersAsync();
        Task<Container?> GetContainerByIdAsync(int id);
        Task<Container> CreateContainerAsync(Container Container);
        Task<Container> UpdateContainerAsync(int Id, Container Container);
        Task<bool> DeleteContainerAsync(int id);

        Task<IEnumerable<Container>> GetContainersByLocationIdAsync(int locationId);
    }
}
