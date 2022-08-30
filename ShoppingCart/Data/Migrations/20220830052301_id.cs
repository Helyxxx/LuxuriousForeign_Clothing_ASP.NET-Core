using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.Data.Migrations
{
    public partial class id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Product",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Category",
                newName: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Product",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Category",
                newName: "CategoryId");
        }
    }
}
