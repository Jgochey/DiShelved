using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiShelved.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategories",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategories", x => new { x.ItemId, x.CategoryId });
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ContainerId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Complete = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "A collection of board games.", "Board Games", 1 },
                    { 2, "Warhammer 40k", "Miniatures", 2 },
                    { 3, "Holiday Decorations", "Decorations", 3 }
                });

            migrationBuilder.InsertData(
                table: "Containers",
                columns: new[] { "Id", "Description", "Image", "LocationId", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "A shelf for board games.", null, 1, "Game Shelf", 1 },
                    { 2, "A box for miniatures.", null, 2, "Miniature Box", 2 },
                    { 3, "A box for holiday decorations.", null, 3, "Holiday Box", 3 }
                });

            migrationBuilder.InsertData(
                table: "ItemCategories",
                columns: new[] { "CategoryId", "ItemId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 5 },
                    { 3, 6 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Complete", "ContainerId", "Description", "Image", "Name", "Quantity", "UserId" },
                values: new object[,]
                {
                    { 1, true, 1, "A board game.", "https://example.com/monopoly.jpg", "Monopoly", 1, 1 },
                    { 2, false, 1, "A second board game.", "https://example.com/parcheesi.jpg", "Parcheesi", 1, 1 },
                    { 3, true, 2, "Minitatures for Warhammer 40k.", "https://example.com/warhammer.jpg", "Space Marines", 12, 2 },
                    { 4, false, 2, "Minitatures for Warhammer 40k.", "https://example.com/eldar.jpg", "Eldar Guardians", 10, 2 },
                    { 5, true, 3, "A Christmas tree.", "https://example.com/christmas_tree.jpg", "Christmas Tree", 1, 3 },
                    { 6, false, 3, "Decorations for Halloween.", "https://example.com/halloween.jpg", "Halloween Decorations", 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "The main living area of the house.", "Living Room", 1 },
                    { 2, "The garage where boxes are stored.", "Garage", 2 },
                    { 3, "The attic where old items are stored.", "Attic", 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                column: "Id",
                values: new object[]
                {
                    1,
                    2,
                    3
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "ItemCategories");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
