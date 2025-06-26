using DiShelved.Models;
using DiShelved.Interfaces;
using DiShelved.DTOs;
using System.Security.Cryptography.X509Certificates;

namespace DiShelved.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _ItemRepository;
        public ItemService(IItemRepository ItemRepository) => _ItemRepository = ItemRepository;

        public async Task<Item?> GetItemByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Item Id", nameof(id));
            }
            var Item = await _ItemRepository.GetItemByIdAsync(id);
            if (Item == null)
            {
                return null;
            }
            return Item;
        }

        public async Task<IEnumerable<Item>> GetItemsByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid User Id", nameof(userId));
            }
            var items = await _ItemRepository.GetItemsByUserIdAsync(userId);
            if (items == null || !items.Any())
            {
                throw new InvalidOperationException("No Items Found for this User Id");
            }
            return items;
        }
        public async Task<Item> CreateItemAsync(Item Item)
        {
            if (Item == null)
            {
                throw new ArgumentNullException(nameof(Item), "Created Item cannot be null");
            }
            var createdItem = await _ItemRepository.CreateItemAsync(Item);
            if (createdItem == null)
            {
                return (Item)Results.BadRequest("Item Not Created");
            }
            return createdItem;
        }

        public async Task<Item> UpdateItemAsync(int id, Item Item)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Item Id", nameof(id));
            }
            if (Item == null || Item.Id <= 0)
            {
                throw new ArgumentNullException("Invalid Item data", nameof(Item));
            }
            var updatedItem = await _ItemRepository.UpdateItemAsync(Item.Id, Item);
            if (updatedItem == null)
            {
                throw new InvalidOperationException("Item could not be updated");
            }
            return updatedItem;
        }
        public async Task<bool> DeleteItemAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Item Id");
            }

            var deleted = await _ItemRepository.DeleteItemAsync(id);
            if (!deleted)
            {
                throw new InvalidOperationException("Item could not be deleted");
            }

            return deleted;
        }
        public async Task<IEnumerable<Item>> GetItemsByContainerIdAsync(int containerId)
        {
            if (containerId <= 0)
            {
                throw new ArgumentException("Invalid Container Id", nameof(containerId));
            }
            var items = await _ItemRepository.GetItemsByContainerIdAsync(containerId);

            return items ?? Enumerable.Empty<Item>();
        }















        // MoveItemDto
        public async Task<Item> MoveItemAsync(int id, int containerId)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Item Id", nameof(id));
            }
            if (containerId <= 0)
            {
                throw new ArgumentException("Invalid Container Id", nameof(containerId));
            }

            var item = await _ItemRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                throw new InvalidOperationException("Item not found");
            }

            item.ContainerId = containerId;
            var updatedItem = await _ItemRepository.UpdateItemAsync(item.Id, item);
            if (updatedItem == null)
            {
                throw new InvalidOperationException("Item could not be moved");
            }

            return updatedItem;
        }

        // Search Items
        public async Task<List<ItemWithCategoriesDTO>> SearchItemsAsync(string searchTerm, int userId)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                throw new ArgumentException("Invalid search term", nameof(searchTerm));
            }
            return await _ItemRepository.SearchItemsAsync(searchTerm, userId);
        }
    }
}
