using DiShelved.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DiShelved.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(int userId);
        Task<IEnumerable<Category>> GetCategoriesByUserUidAsync(string uid);
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategoryAsync(Category Category);
        Task<Category> UpdateCategoryAsync(int Id, Category Category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
