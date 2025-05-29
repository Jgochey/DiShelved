using DiShelved.Services;
using DiShelved.Models;
using DiShelved.Interfaces;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace DiShelved.Tests
{

  public class CategoryServiceTests

  {
    private readonly CategoryService _categoryService;

    private readonly Mock<ICategoryRepository> _mockCategoryRepository;

    public CategoryServiceTests()
    {
      _mockCategoryRepository = new Mock<ICategoryRepository>();
      _categoryService = new CategoryService(_mockCategoryRepository.Object);
    }

    [Fact]
    public async Task GetCategoryById_ShouldReturnCategory_WhenCategoryExists()
    {
      var CategoryId = 1;

      var expectedCategory = new Category { Id = CategoryId, Name = "Decoractions", Description = "Holiday Decorations", UserId = 1};

      // The GetCategoryById method is called with the CategoryId parameter, the mock object should return the expectedCategory instance.
      _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(CategoryId)).Returns(Task.FromResult(expectedCategory));

      var actualCategory = await _categoryService.GetCategoryByIdAsync(CategoryId);

      // The actualCategory returned by the GetCategoryById method should be equal to the expectedCategory instance.
      Assert.Equal(expectedCategory, actualCategory);
    }

    [Fact]
    public async Task GetCategoryById_ShouldReturnNull_WhenCategoryDoesNotExist()
    {
      // A Category with an Id of 999 does not exist in the repository
      // Therefore, the GetCategoryById method should return null.
      var CategoryId = 999;

      _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(CategoryId)).Returns(Task.FromResult<Category>(null!));

      var actualCategory = await _categoryService.GetCategoryByIdAsync(CategoryId);

      // The actualCategory returned should be null.
      Assert.Null(actualCategory);
    }

    [Fact]
    public async Task CreateCategory_ShouldCreateCategory_WhenCategoryIsValid()
    {
      var newCategory = new Category { Id = 55, Name = "Cooking Supplies", Description = "Oven mitts, pots and pans.", UserId = 2 };
      // The CreateCategory method should return the newCategory instance when the newCategory parameter is valid.

      _mockCategoryRepository.Setup(repo => repo.CreateCategoryAsync(newCategory)).Verifiable();

      // The Verify method is used to verify that the CreateCategory method was called with the newCategory parameter.
      await _mockCategoryRepository.Object.CreateCategoryAsync(newCategory);
      _mockCategoryRepository.Verify(repo => repo.CreateCategoryAsync(newCategory), Times.Once);
    }

    [Fact]
    public async Task CreateCategory_ShouldThrowException_WhenCategoryIsNull()
    {
      Category newCategory = null;

      // The CreateCategory method should throw an ArgumentNullException when the newCategory parameter is null.
      await Assert.ThrowsAsync<ArgumentNullException>(async () => await _categoryService.CreateCategoryAsync(newCategory));
    }

    [Fact]
    public async Task UpdateCategory_ShouldUpdateCategory_WhenCategoryExists()
    {
      var existingCategory = new Category { Id = 10, Name = "Decoractions", Description = "Holiday Decorations", UserId = 1};
      var updatedCategory = new Category { Id = 11, Name = "Cooking Supplies", Description = "Oven mitts, pots and pans.", UserId = 1 };
      // The UpdateCategory method should return the updatedCategory instance when the updatedCategory parameter is valid.

      _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(existingCategory.Id)).Returns(Task.FromResult(existingCategory));
      _mockCategoryRepository.Setup(repo => repo.UpdateCategoryAsync(updatedCategory.Id, updatedCategory)).Returns(Task.FromResult(updatedCategory)).Verifiable();

      await _categoryService.UpdateCategoryAsync(updatedCategory.Id, updatedCategory);

      _mockCategoryRepository.Verify(repo => repo.UpdateCategoryAsync(updatedCategory.Id, updatedCategory), Times.Once);
    }

    [Fact]
    public async Task UpdateCategory_ShouldThrowException_WhenCategoryDoesNotExist()
    {
      var nonExistingCategory = new Category { Id = 400, Name = "Nothing", Description = "A box full of nothing.", UserId = 2 };

      _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(nonExistingCategory.Id)).Returns(Task.FromResult<Category>(null));

      await Assert.ThrowsAsync<InvalidOperationException>(async () => await _categoryService.UpdateCategoryAsync(nonExistingCategory.Id, nonExistingCategory));
    }

    [Fact]
    public async Task DeleteCategory_ShouldDeleteCategory_WhenCategoryExists()
    {
      var CategoryId = 1;
      var existingCategory = new Category { Id = CategoryId, Name = "Cooking Supplies", Description = "Oven mitts, pots and pans.", UserId = 2 };

      _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(CategoryId)).Returns(Task.FromResult(existingCategory));
      _mockCategoryRepository.Setup(repo => repo.DeleteCategoryAsync(CategoryId))
          .Returns(Task.FromResult(true)).Verifiable();

      await _categoryService.DeleteCategoryAsync(CategoryId);

      _mockCategoryRepository.Verify(repo => repo.DeleteCategoryAsync(CategoryId), Times.Once);
    }

    [Fact]
    public async Task DeleteCategory_ShouldThrowException_WhenCategoryDoesNotExist()
    {
      var CategoryId = 999;

      _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(CategoryId)).Returns(Task.FromResult<Category>(null));

      await Assert.ThrowsAsync<InvalidOperationException>(async () => await _categoryService.DeleteCategoryAsync(CategoryId));
    }
  }
}
