using DiShelved.Models;
using DiShelved.Interfaces;

namespace DiShelved.Services
{
    public class ItemCategoryService : IItemCategoryService
    {
        private readonly IItemCategoryRepository _ItemCategoryRepository;
        public ItemCategoryService(IItemCategoryRepository ItemCategoryRepository) => _ItemCategoryRepository = ItemCategoryRepository;

        public async Task<ItemCategory> CreateItemCategoryAsync(ItemCategory ItemCategory)
        {
            if (ItemCategory == null)
            {
                throw new ArgumentNullException(nameof(ItemCategory), "Created ItemCategory cannot be null");
            }
            var createdItemCategory = await _ItemCategoryRepository.CreateItemCategoryAsync(ItemCategory);
            if (createdItemCategory == null)
            {
                return (ItemCategory)Results.BadRequest("ItemCategory Not Created");
            }
            return createdItemCategory;
        }

        public async Task<bool> DeleteItemCategoryAsync(int itemId, int categoryId)
        {
            var itemCategory = await _ItemCategoryRepository.GetItemCategoryByIdAsync(itemId, categoryId);

            if (itemId <= 0)
            {
                throw new ArgumentException("Invalid Item Id", nameof(itemId));
            }
            if (categoryId <= 0)
            {
                throw new ArgumentException("Invalid Category Id", nameof(categoryId));
            }
            if (itemCategory == null)
            {
                throw new InvalidOperationException("ItemCategory not found for the provided Item Id and Category Id");
            }

            var isDeleted = await _ItemCategoryRepository.DeleteItemCategoryAsync(itemId, categoryId);
            if (!isDeleted)
            {
                return false;
            }
            return true;
        }

        public async Task<ItemCategory?> GetItemCategoryByIdAsync(int itemId, int categoryId)
        {
            if (itemId <= 0)
            {
                throw new ArgumentException("Invalid Item Id", nameof(itemId));
            }
            if (categoryId <= 0)
            {
                throw new ArgumentException("Invalid Category Id", nameof(categoryId));
            }
            var itemCategory = await _ItemCategoryRepository.GetItemCategoryByIdAsync(itemId, categoryId);
            if (itemCategory == null)
            {
                return null;
            }
            return itemCategory;
        }

        public async Task<IEnumerable<ItemCategory>> GetItemCategoriesByItemIdAsync(int itemId)
        {
            if (itemId <= 0)
            {
                throw new ArgumentException("Invalid Item Id", nameof(itemId));
            }
            var itemCategories = await _ItemCategoryRepository.GetItemCategoriesByItemIdAsync(itemId);
            if (itemCategories == null || !itemCategories.Any())
            {
                return Enumerable.Empty<ItemCategory>();
            }
            return itemCategories;
        }
        public async Task<IEnumerable<ItemCategory>> GetItemCategoriesByCategoryIdAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("Invalid Category Id", nameof(categoryId));
            }
            var itemCategories = await _ItemCategoryRepository.GetItemCategoriesByCategoryIdAsync(categoryId);
            if (itemCategories == null || !itemCategories.Any())
            {
                return Enumerable.Empty<ItemCategory>();
            }
            return itemCategories;
        }

        // public Task<ItemCategory> UpdateItemCategoryAsync(int itemId, int categoryId, ItemCategory itemCategory)
        // {
        //   throw new NotImplementedException();
        // }

    }
}
