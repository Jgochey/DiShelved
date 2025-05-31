using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiShelved.Migrations
{
    /// <inheritdoc />
    public partial class UidForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uid",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Uid",
                value: "6KTKbh6BBYMXkqjJ0oYdEsb3ekC2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Uid",
                value: "dTsb3ekC26DKGMXkqj6KTKbhJ0oY");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Uid",
                value: "dHsb3ekC26DVBSXkqj6KPLbhJ0oY");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uid",
                table: "Users");
        }
    }
}
