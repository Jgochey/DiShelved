using DiShelved.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DiShelved.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(int id);
        Task<Item> CreateItemAsync(Item Item);
        Task<Item> UpdateItemAsync(int Id, Item Item);
        Task<bool> DeleteItemAsync(int id);
        Task<IEnumerable<Item>> GetItemsByContainerIdAsync(int containerId);
    }
}
