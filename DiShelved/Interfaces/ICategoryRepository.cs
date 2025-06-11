using DiShelved.Models;

namespace DiShelved.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(int userId);
        Task<IEnumerable<Category>> GetCategoriesByUserUidAsync(string uid);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategoryAsync(Category Category);
        Task<Category> UpdateCategoryAsync(int id, Category Category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
