using DiShelved.Models;

namespace DiShelved.Interfaces
{
    public interface IContainerRepository
    {

        Task<IEnumerable<Container>> GetContainersByUserIdAsync(int userId);
        Task<Container> GetContainerByIdAsync(int id);
        Task<Container> CreateContainerAsync(Container Container);
        Task<Container> UpdateContainerAsync(int id, Container Container);
        Task<bool> DeleteContainerAsync(int id);
        Task<IEnumerable<Container>> GetContainersByLocationIdAsync(int locationId);
    }
}
