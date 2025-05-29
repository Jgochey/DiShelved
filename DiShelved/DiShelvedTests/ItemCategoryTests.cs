using DiShelved.Services;
using DiShelved.Models;
using DiShelved.Interfaces;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace DiShelved.Tests
{

  public class ItemCategoryServiceTests

  {
    private readonly ItemCategoryService _itemCategoryService;

    private readonly Mock<IItemCategoryRepository> _mockItemCategoryRepository;

    public ItemCategoryServiceTests()
    {
      _mockItemCategoryRepository = new Mock<IItemCategoryRepository>();
      _itemCategoryService = new ItemCategoryService(_mockItemCategoryRepository.Object);
    }

    [Fact]
    public async Task CreateItemCategory_ShouldCreateItemCategory_WhenItemCategoryIsValid()
    {
      var newItemCategory = new ItemCategory { ItemId = 1, CategoryId = 2 };
      // The CreateItemCategory method should return the newItemCategory instance when the newItemCategory parameter is valid.

      _mockItemCategoryRepository.Setup(repo => repo.CreateItemCategoryAsync(newItemCategory)).Verifiable();

      // The Verify method is used to verify that the CreateItemCategory method was called with the newItemCategory parameter.
      await _mockItemCategoryRepository.Object.CreateItemCategoryAsync(newItemCategory);
      _mockItemCategoryRepository.Verify(repo => repo.CreateItemCategoryAsync(newItemCategory), Times.Once);
    }

    [Fact]
    public async Task CreateItemCategory_ShouldThrowException_WhenItemCategoryIsNull()
    {
      ItemCategory newItemCategory = null;

      // The CreateItemCategory method should throw an ArgumentNullException when the newItemCategory parameter is null.
      await Assert.ThrowsAsync<ArgumentNullException>(async () => await _itemCategoryService.CreateItemCategoryAsync(newItemCategory));
    }

    [Fact]
    public async Task DeleteItemCategory_ShouldDeleteItemCategory_WhenItemCategoryExists()
    {
      var Item = 1;
      var Category = 2;
      var existingItemCategory = new ItemCategory { ItemId = Item, CategoryId = Category };

      _mockItemCategoryRepository.Setup(repo => repo.GetItemCategoryByIdAsync(Item, Category)).Returns(Task.FromResult(existingItemCategory));
      _mockItemCategoryRepository.Setup(repo => repo.DeleteItemCategoryAsync(Item, Category))
          .Returns(Task.FromResult(true)).Verifiable();

      await _itemCategoryService.DeleteItemCategoryAsync(Item, Category);

      _mockItemCategoryRepository.Verify(repo => repo.DeleteItemCategoryAsync(Item, Category), Times.Once);
    }

    [Fact]
    public async Task DeleteItemCategory_ShouldThrowException_WhenItemCategoryDoesNotExist()
    {
      var Item = 999;
      var Category = 999;

      _mockItemCategoryRepository.Setup(repo => repo.GetItemCategoryByIdAsync(Item, Category)).Returns(Task.FromResult<ItemCategory>(null));

      await Assert.ThrowsAsync<InvalidOperationException>(async () => await _itemCategoryService.DeleteItemCategoryAsync(Item, Category));
    }
  }
}
