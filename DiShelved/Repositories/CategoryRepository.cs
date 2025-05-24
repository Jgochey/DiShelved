using Microsoft.EntityFrameworkCore;
using DiShelved.Models;
using DiShelved.Interfaces;
using DiShelved.Data;

namespace DiShelved.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DiShelvedDbContext _context;
        public CategoryRepository(DiShelvedDbContext context) => _context = context;
       
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            if (id <= 0)
            {
                return (Category)Results.BadRequest("Category Id not found");
            }
            var Category = await _context.Categories.FindAsync(id);
            if (Category == null)
            {
                return (Category)Results.BadRequest("Category not found");
            }
            return Category;
        }
        public async Task<Category> CreateCategoryAsync(Category Category)
        {
            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();
            return Category;
        }
        public async Task<Category> UpdateCategoryAsync(int id, Category Category)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return (Category)Results.BadRequest("Category not found");
            }

            existingCategory.Name = Category.Name;
            existingCategory.Description = Category.Description;
            existingCategory.UserId = Category.UserId;

            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var Category = await _context.Categories.FindAsync(id);
            if (Category == null)
            {
                return false;
            }
            _context.Categories.Remove(Category);
            await _context.SaveChangesAsync();
            return true;
        }
  }
}
