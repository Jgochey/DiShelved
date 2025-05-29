using Microsoft.EntityFrameworkCore;
using DiShelved.Models;
using DiShelved.Interfaces;
using DiShelved.Data;

namespace DiShelved.Repositories
{
    public class ItemCategoryRepository : IItemCategoryRepository
    {
        private readonly DiShelvedDbContext _context;
        public ItemCategoryRepository(DiShelvedDbContext context) => _context = context;

        public async Task<ItemCategory> CreateItemCategoryAsync(ItemCategory ItemCategory)
        {
            _context.ItemCategories.Add(ItemCategory);
            await _context.SaveChangesAsync();
            return ItemCategory;
        }

        public async Task<bool> DeleteItemCategoryAsync(int itemId, int categoryId)
        {
            var itemCategory = await _context.ItemCategories
                .FirstOrDefaultAsync(ic => ic.ItemId == itemId && ic.CategoryId == categoryId);

            if (itemCategory == null)
            {
                return false;
            }

            _context.ItemCategories.Remove(itemCategory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ItemCategory?> GetItemCategoryByIdAsync(int itemId, int categoryId)
        {
            return await _context.ItemCategories
                .FirstOrDefaultAsync(ic => ic.ItemId == itemId && ic.CategoryId == categoryId);
        }
        
        // public async Task<ItemCategory> UpdateItemCategoryAsync(int id, ItemCategory ItemCategory)
        // {
        //     if (id != ItemCategory.Id)
        //     {
        //         throw new ArgumentException("ItemCategory Id mismatch");
        //     }
        //     _context.Entry(ItemCategory).State = EntityState.Modified;
        //     await _context.SaveChangesAsync();
        //     return ItemCategory;
        // }
    }
}
