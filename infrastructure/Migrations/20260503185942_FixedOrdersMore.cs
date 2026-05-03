using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedOrdersMore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineSum",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ProductOrdered_ProductId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "ProductOrdered_ProductName",
                table: "OrderItems",
                newName: "ProductName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "OrderItems",
                newName: "ProductOrdered_ProductName");

            migrationBuilder.AddColumn<decimal>(
                name: "LineSum",
                table: "OrderItems",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductOrdered_ProductId",
                table: "OrderItems",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
