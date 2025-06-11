using DiShelved.Models;
using DiShelved.Interfaces;

namespace DiShelved.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _CategoryRepository;
        public CategoryService(ICategoryRepository CategoryRepository) => _CategoryRepository = CategoryRepository;

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            if (id <= 0)
            {
            throw new ArgumentException("Invalid Category Id", nameof(id));
            }
            var Category = await _CategoryRepository.GetCategoryByIdAsync(id);
            if (Category == null)
            {
                return null;
            }
            return Category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid User Id", nameof(userId));
            }
            var categories = await _CategoryRepository.GetCategoriesByUserIdAsync(userId);
            // Return an empty list if the user has no categories
            return categories ?? new List<Category>();
        }

        public async Task<IEnumerable<Category>> GetCategoriesByUserUidAsync(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                throw new ArgumentException("Invalid User Uid", nameof(uid));
            }
            var categories = await _CategoryRepository.GetCategoriesByUserUidAsync(uid);
            // Return an empty list if the user has no categories
            return categories ?? new List<Category>();
        }

        public async Task<Category> CreateCategoryAsync(Category Category)
        {
            if (Category == null)
            {
                throw new ArgumentNullException(nameof(Category), "Created Category cannot be null");
            }
            var createdCategory = await _CategoryRepository.CreateCategoryAsync(Category);
            if (createdCategory == null)
            {
                return (Category)Results.BadRequest("Category Not Created");
            }
            return createdCategory;
        }

        public async Task<Category> UpdateCategoryAsync(int id, Category Category)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Category Id", nameof(id));
            }
            if (Category == null || Category.Id <= 0)
            {
                throw new ArgumentNullException("Invalid Category data", nameof(Category));
            }
            var updatedCategory = await _CategoryRepository.UpdateCategoryAsync(Category.Id, Category);
            if (updatedCategory == null)
            {
                throw new InvalidOperationException("Category could not be updated");
            }
            return updatedCategory;
        }
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Category Id");
            }

            var deleted = await _CategoryRepository.DeleteCategoryAsync(id);
            if (!deleted)
            {
                throw new InvalidOperationException("Category could not be deleted");
            }

            return deleted;
        }
    }
}
