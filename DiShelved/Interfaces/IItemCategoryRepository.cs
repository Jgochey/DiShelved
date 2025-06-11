using DiShelved.Models;

namespace DiShelved.Interfaces
{
    public interface IItemCategoryRepository
    {
        Task<ItemCategory> CreateItemCategoryAsync(ItemCategory itemCategory);
        Task<bool> DeleteItemCategoryAsync(int itemId, int categoryId);

        Task<ItemCategory?> GetItemCategoryByIdAsync(int itemId, int categoryId);

        Task<IEnumerable<ItemCategory>> GetItemCategoriesByItemIdAsync(int itemId);
        Task<IEnumerable<ItemCategory>> GetItemCategoriesByCategoryIdAsync(int categoryId);

        // Task<ItemCategory> UpdateItemCategoryAsync(int id, ItemCategory itemCategory);
    }
}
