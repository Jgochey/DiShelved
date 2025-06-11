using DiShelved.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DiShelved.Interfaces
{
  public interface IItemCategoryService
  {
    Task<ItemCategory> CreateItemCategoryAsync(ItemCategory ItemCategory);
    Task<bool> DeleteItemCategoryAsync(int itemId, int categoryId);

    Task<ItemCategory?> GetItemCategoryByIdAsync(int itemId, int categoryId);

    Task<IEnumerable<ItemCategory>> GetItemCategoriesByItemIdAsync(int itemId);
    Task<IEnumerable<ItemCategory>> GetItemCategoriesByCategoryIdAsync(int categoryId);

    // Task<ItemCategory> UpdateItemCategoryAsync(int itemId, int categoryId, ItemCategory itemCategory);
  }
}
