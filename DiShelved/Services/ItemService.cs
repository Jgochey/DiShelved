using DiShelved.Models;
using DiShelved.Interfaces;

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
        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            var Items = await _ItemRepository.GetAllItemsAsync();
            if (Items == null || !Items.Any())
            {
                throw new InvalidOperationException("No Items Found");
            }
            return Items;
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
    }
}
