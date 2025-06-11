using DiShelved.Models;

namespace DiShelved.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetItemsByUserIdAsync(int userId);
        Task<Item> GetItemByIdAsync(int id);
        Task<Item> CreateItemAsync(Item Item);
        Task<Item> UpdateItemAsync(int id, Item Item);
        Task<bool> DeleteItemAsync(int id);

        Task<IEnumerable<Item>> GetItemsByContainerIdAsync(int containerId);

        Task<Item> MoveItemAsync(int id, int containerId);
        Task<List<Item>> SearchItemsAsync(string searchTerm, int userId);
    }
}
