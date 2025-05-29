using DiShelved.Models;

namespace DiShelved.Interfaces
{
    public interface IItemCategoryRepository
    {
        Task<ItemCategory> CreateItemCategoryAsync(ItemCategory itemCategory);
        Task<bool> DeleteItemCategoryAsync(int itemId, int categoryId);

        // Task<ItemCategory> UpdateItemCategoryAsync(int id, ItemCategory itemCategory);
    }
}
