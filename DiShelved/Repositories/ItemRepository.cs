﻿using Microsoft.EntityFrameworkCore;
using DiShelved.Models;
using DiShelved.Interfaces;
using DiShelved.Data;
using DiShelved.DTOs;

namespace DiShelved.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DiShelvedDbContext _context;
        public ItemRepository(DiShelvedDbContext context) => _context = context;

        public async Task<IEnumerable<Item>> GetItemsByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                return (IEnumerable<Item>)Results.BadRequest("User Id not found");
            }
            var items = await _context.Items
                .Where(i => i.UserId == userId)
                .ToListAsync();
            if (items == null || !items.Any())
            {
                return (IEnumerable<Item>)Results.BadRequest("Items not found for this User Id");
            }
            return items;
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

        public Task<IEnumerable<Item>> GetItemsByContainerIdAsync(int containerId)
        {
            if (containerId <= 0)
            {
                return Task.FromResult<IEnumerable<Item>>(new List<Item>());
            }
            return _context.Items.Where(i => i.ContainerId == containerId).ToListAsync().ContinueWith(task => (IEnumerable<Item>)task.Result);
        }

        // MoveItemDto
        public async Task<Item> MoveItemAsync(int id, int containerId)
        {
            if (id <= 0)
            {
                return (Item)Results.BadRequest("Invalid Item Id");
            }
            if (containerId <= 0)
            {
                return (Item)Results.BadRequest("Invalid Container Id");
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return (Item)Results.BadRequest("Item not found");
            }

            item.ContainerId = containerId;
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        // Search Items
        public async Task<List<ItemWithCategoriesDTO>> SearchItemsAsync(string searchTerm, int userId)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<ItemWithCategoriesDTO>();
            }

            searchTerm = searchTerm.ToLower();

            // Find ItemIds by Category name (case-insensitive)
            var categoryItemIds = await _context.ItemCategories
                .Where(ic => ic.Item.UserId == userId && ic.Category.Name.ToLower().Contains(searchTerm))
                .Select(ic => ic.ItemId)
                .ToListAsync();

            // Find Items by name, description, or category (case-insensitive)
            var items = await _context.Items
                .Where(i => i.UserId == userId &&
                    (i.Name.ToLower().Contains(searchTerm) ||
                     i.Description.ToLower().Contains(searchTerm) ||
                     categoryItemIds.Contains(i.Id)))
                .ToListAsync();

            var itemIds = items.Select(i => i.Id).ToList();

            var itemCategories = await _context.ItemCategories
                .Where(ic => itemIds.Contains(ic.ItemId))
                .Include(ic => ic.Category)
                .ToListAsync();

            var result = items.Select(item => new ItemWithCategoriesDTO
            {
                Item = item,
                Categories = itemCategories
                    .Where(ic => ic.ItemId == item.Id)
                    .Select(ic => ic.Category)
                    .ToList()
            }).ToList();

            return result;
        }
    }
}
