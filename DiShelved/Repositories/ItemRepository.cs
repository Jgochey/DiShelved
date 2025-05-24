using Microsoft.EntityFrameworkCore;
using DiShelved.Models;
using DiShelved.Interfaces;
using DiShelved.Data;

namespace DiShelved.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DiShelvedDbContext _context;
        public ItemRepository(DiShelvedDbContext context) => _context = context;
       
        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }
        public async Task<Item> GetItemByIdAsync(int id)
        {
            if (id <= 0)
            {
                return (Item)Results.BadRequest("Item Id not found");
            }
            var Item = await _context.Items.FindAsync(id);
            if (Item == null)
            {
                return (Item)Results.BadRequest("Item not found");
            }
            return Item;
        }
        public async Task<Item> CreateItemAsync(Item Item)
        {
            _context.Items.Add(Item);
            await _context.SaveChangesAsync();
            return Item;
        }
        public async Task<Item> UpdateItemAsync(int id, Item Item)
        {
            var existingItem = await _context.Items.FindAsync(id);
            if (existingItem == null)
            {
                return (Item)Results.BadRequest("Item not found");
            }

            existingItem.Name = Item.Name;
            existingItem.Description = Item.Description;
            existingItem.ContainerId = Item.ContainerId;
            existingItem.Quantity = Item.Quantity;
            existingItem.Complete = Item.Complete;
            existingItem.UserId = Item.UserId;
            existingItem.Image = Item.Image;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var Item = await _context.Items.FindAsync(id);
            if (Item == null)
            {
                return false;
            }
            _context.Items.Remove(Item);
            await _context.SaveChangesAsync();
            return true;
        }
  }
}
