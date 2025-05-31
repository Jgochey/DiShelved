using DiShelved.Models;
using Microsoft.EntityFrameworkCore;

namespace DiShelved.Data
{

    public class DiShelvedDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }

        // Taking the options parameter and passing it to the base class constructor.
        // This allows the DbContext to be configured with options such as the database provider and connection string.
        public DiShelvedDbContext(DbContextOptions<DiShelvedDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemCategory>()
                .HasKey(ic => new { ic.ItemId, ic.CategoryId });

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Uid = "6KTKbh6BBYMXkqjJ0oYdEsb3ekC2" },
                new User { Id = 2, Uid = "dTsb3ekC26DKGMXkqj6KTKbhJ0oY" },
                new User { Id = 3, Uid = "dHsb3ekC26DVBSXkqj6KPLbhJ0oY" }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Board Games", Description = "A collection of board games.", UserId = 1 },
                new Category { Id = 2, Name = "Miniatures", Description = "Warhammer 40k", UserId = 2 },
                new Category { Id = 3, Name = "Decorations", Description = "Holiday Decorations", UserId = 3 }
            );

            // Seed Containers
            modelBuilder.Entity<Container>().HasData(
                new Container { Id = 1, Name = "Game Shelf", Description = "A shelf for board games.", LocationId = 1, UserId = 1 },
                new Container { Id = 2, Name = "Miniature Box", Description = "A box for miniatures.", LocationId = 2, UserId = 2 },
                new Container { Id = 3, Name = "Holiday Box", Description = "A box for holiday decorations.", LocationId = 3, UserId = 3 }
            );

            // Seed Items
            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Monopoly", Description = "A board game.", ContainerId = 1, Quantity = 1, Complete = true, UserId = 1, Image = "https://example.com/monopoly.jpg" },
                new Item { Id = 2, Name = "Parcheesi", Description = "A second board game.", ContainerId = 1, Quantity = 1, Complete = false, UserId = 1, Image = "https://example.com/parcheesi.jpg" },
                new Item { Id = 3, Name = "Space Marines", Description = "Minitatures for Warhammer 40k.", ContainerId = 2, Quantity = 12, Complete = true, UserId = 2, Image = "https://example.com/warhammer.jpg" },
                new Item { Id = 4, Name = "Eldar Guardians", Description = "Minitatures for Warhammer 40k.", ContainerId = 2, Quantity = 10, Complete = false, UserId = 2, Image = "https://example.com/eldar.jpg" },
                new Item { Id = 5, Name = "Christmas Tree", Description = "A Christmas tree.", ContainerId = 3, Quantity = 1, Complete = true, UserId = 3, Image = "https://example.com/christmas_tree.jpg" },
                new Item { Id = 6, Name = "Halloween Decorations", Description = "Decorations for Halloween.", ContainerId = 3, Quantity = 1, Complete = false, UserId = 3, Image = "https://example.com/halloween.jpg" }

            );

            // Seed ItemCategory Join Table
            // This is a many-to-many relationship between Items and Categories
            modelBuilder.Entity<ItemCategory>().HasData(
                new ItemCategory { ItemId = 1, CategoryId = 1 },
                new ItemCategory { ItemId = 2, CategoryId = 1 },
                new ItemCategory { ItemId = 3, CategoryId = 2 },
                new ItemCategory { ItemId = 4, CategoryId = 2 },
                new ItemCategory { ItemId = 5, CategoryId = 3 },
                new ItemCategory { ItemId = 6, CategoryId = 3 }
            );

            // Seed Locations
            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Name = "Living Room", Description = "The main living area of the house.", UserId = 1 },
                new Location { Id = 2, Name = "Garage", Description = "The garage where boxes are stored.", UserId = 2 },
                new Location { Id = 3, Name = "Attic", Description = "The attic where old items are stored.", UserId = 3 }
            );
        }
    }
}
