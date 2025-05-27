using DiShelved.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DiShelved.Interfaces
{
    public interface IItemCategoryService
    {
        // Testing
        Task<IEnumerable<ItemCategory>> GetAllItemCategoriesAsync();

        // MVP
        Task<ItemCategory> CreateItemCategoryAsync(ItemCategory ItemCategory);
        Task<bool> DeleteItemCategoryAsync(int itemId, int categoryId);

        // Stretch
        // Task<ItemCategory> UpdateItemCategoryAsync(int itemId, int categoryId, ItemCategory itemCategory);
  }
}
