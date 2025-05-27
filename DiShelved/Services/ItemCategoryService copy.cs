using DiShelved.Models;
using DiShelved.Interfaces;

namespace DiShelved.Services
{
    public class ItemCategoryService : IItemCategoryService
    {
        private readonly IItemCategoryRepository _ItemCategoryRepository;
        public ItemCategoryService(IItemCategoryRepository ItemCategoryRepository) => _ItemCategoryRepository = ItemCategoryRepository;

        public async Task<IEnumerable<ItemCategory>> GetAllItemCategoriesAsync()
        {
            var ItemCategories = await _ItemCategoryRepository.GetAllItemCategoriesAsync();
            if (ItemCategories == null || !ItemCategories.Any())
            {
                throw new InvalidOperationException("No Item Categories Found");
            }
            return ItemCategories;
        }
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
            if (itemId <= 0)
            {
                throw new ArgumentException("Invalid Item Id", nameof(itemId));
            }
            if (categoryId <= 0)
            {
                throw new ArgumentException("Invalid Category Id", nameof(categoryId));
            }
            var isDeleted = await _ItemCategoryRepository.DeleteItemCategoryAsync(itemId, categoryId);
            if (!isDeleted)
            {
                return false;
            }
            return true;
        }

        // public Task<ItemCategory> UpdateItemCategoryAsync(int itemId, int categoryId, ItemCategory itemCategory)
        // {
        //   throw new NotImplementedException();
        // }
    
  }
}
