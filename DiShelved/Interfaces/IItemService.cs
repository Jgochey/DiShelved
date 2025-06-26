using DiShelved.Models;
using DiShelved.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DiShelved.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetItemsByUserIdAsync(int userId);
        Task<Item?> GetItemByIdAsync(int id);
        Task<Item> CreateItemAsync(Item Item);
        Task<Item> UpdateItemAsync(int Id, Item Item);
        Task<bool> DeleteItemAsync(int id);
        Task<IEnumerable<Item>> GetItemsByContainerIdAsync(int containerId);
        Task<Item> MoveItemAsync(int id, int containerId);
        Task<List<ItemWithCategoriesDTO>> SearchItemsAsync(string searchTerm, int userId);
    }
}
