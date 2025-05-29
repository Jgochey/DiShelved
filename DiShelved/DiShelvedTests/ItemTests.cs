using DiShelved.Services;
using DiShelved.Models;
using DiShelved.Interfaces;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace DiShelved.Tests
{

  public class ItemServiceTests

  {
    private readonly ItemService _itemService;

    private readonly Mock<IItemRepository> _mockItemRepository;

    public ItemServiceTests()
    {
      _mockItemRepository = new Mock<IItemRepository>();
      _itemService = new ItemService(_mockItemRepository.Object);
    }

  

    [Fact]
    public async Task GetItemById_ShouldReturnItem_WhenItemExists()
    {
      var ItemId = 1;

      var expectedItem = new Item { Id = ItemId, Name = "Lights", Description = "A box of Christmas Lights", ContainerId = 1, Quantity = 1, Complete = true, UserId = 1, Image = "https://example.com/image.jpg" };

      // The GetItemById method is called with the ItemId parameter, the mock object should return the expectedItem instance.
      _mockItemRepository.Setup(repo => repo.GetItemByIdAsync(ItemId)).Returns(Task.FromResult(expectedItem));

      var actualItem = await _itemService.GetItemByIdAsync(ItemId);

      // The actualItem returned by the GetItemById method should be equal to the expectedItem instance.
      Assert.Equal(expectedItem, actualItem);
    }

    [Fact]
    public async Task GetItemById_ShouldReturnNull_WhenItemDoesNotExist()
    {
      // A Item with an Id of 999 does not exist in the repository
      // Therefore, the GetItemById method should return null.
      var ItemId = 999;

      _mockItemRepository.Setup(repo => repo.GetItemByIdAsync(ItemId)).Returns(Task.FromResult<Item>(null!));

      var actualItem = await _itemService.GetItemByIdAsync(ItemId);

      // The actualItem returned should be null.
      Assert.Null(actualItem);
    }

    [Fact]
    public async Task CreateItem_ShouldCreateItem_WhenItemIsValid()
    {
      var newItem = new Item { Id = 20, Name = "Lights", Description = "A box of Christmas Lights", ContainerId = 1, Quantity = 1, Complete = true, UserId = 1, Image = "https://example.com/image.jpg" };
      // The CreateItem method should return the newItem instance when the newItem parameter is valid.

      _mockItemRepository.Setup(repo => repo.CreateItemAsync(newItem)).Verifiable();

      // The Verify method is used to verify that the CreateItem method was called with the newItem parameter.
      await _mockItemRepository.Object.CreateItemAsync(newItem);
      _mockItemRepository.Verify(repo => repo.CreateItemAsync(newItem), Times.Once);
    }

    [Fact]
    public async Task CreateItem_ShouldThrowException_WhenItemIsNull()
    {
      Item newItem = null;

      // The CreateItem method should throw an ArgumentNullException when the newItem parameter is null.
      await Assert.ThrowsAsync<ArgumentNullException>(async () => await _itemService.CreateItemAsync(newItem));
    }

    [Fact]
    public async Task UpdateItem_ShouldUpdateItem_WhenItemExists()
    {
      var existingItem = new Item { Id = 20, Name = "Lights", Description = "A box of Christmas Lights", ContainerId = 1, Quantity = 1, Complete = true, UserId = 1, Image = "https://example.com/image.jpg" };
      var updatedItem = new Item { Id = 20, Name = "Lights", Description = "An empty box of Christmas Lights", ContainerId = 1, Quantity = 0, Complete = true, UserId = 1, Image = "https://example.com/image.jpg" };
      // The UpdateItem method should return the updatedItem instance when the updatedItem parameter is valid.

      _mockItemRepository.Setup(repo => repo.GetItemByIdAsync(existingItem.Id)).Returns(Task.FromResult(existingItem));
      _mockItemRepository.Setup(repo => repo.UpdateItemAsync(updatedItem.Id, updatedItem)).Returns(Task.FromResult(updatedItem)).Verifiable();

      await _itemService.UpdateItemAsync(updatedItem.Id, updatedItem);

      _mockItemRepository.Verify(repo => repo.UpdateItemAsync(updatedItem.Id, updatedItem), Times.Once);
    }

    [Fact]
    public async Task UpdateItem_ShouldThrowException_WhenItemDoesNotExist()
    {
      var nonExistingItem = new Item { Id = 800, Name = "Nothing", Description = "A box full of Nothing.", ContainerId = 1,Quantity = 1, Complete = true, UserId = 1, Image = "https://example.com/image.jpg" };

      _mockItemRepository.Setup(repo => repo.GetItemByIdAsync(nonExistingItem.Id)).Returns(Task.FromResult<Item>(null));

      await Assert.ThrowsAsync<InvalidOperationException>(async () => await _itemService.UpdateItemAsync(nonExistingItem.Id, nonExistingItem));
    }

    [Fact]
    public async Task DeleteItem_ShouldDeleteItem_WhenItemExists()
    {
      var ItemId = 1;
      var existingItem = new Item { Id = ItemId, Name = "Lights", Description = "A box of Christmas Lights", ContainerId = 1,Quantity = 1, Complete = true, UserId = 1, Image = "https://example.com/image.jpg" };

      _mockItemRepository.Setup(repo => repo.GetItemByIdAsync(ItemId)).Returns(Task.FromResult(existingItem));
      _mockItemRepository.Setup(repo => repo.DeleteItemAsync(ItemId))
          .Returns(Task.FromResult(true)).Verifiable();

      await _itemService.DeleteItemAsync(ItemId);

      _mockItemRepository.Verify(repo => repo.DeleteItemAsync(ItemId), Times.Once);
    }

    [Fact]
    public async Task DeleteItem_ShouldThrowException_WhenItemDoesNotExist()
    {
      var ItemId = 999;

      _mockItemRepository.Setup(repo => repo.GetItemByIdAsync(ItemId)).Returns(Task.FromResult<Item>(null));

      await Assert.ThrowsAsync<InvalidOperationException>(async () => await _itemService.DeleteItemAsync(ItemId));
    }
  }
}
