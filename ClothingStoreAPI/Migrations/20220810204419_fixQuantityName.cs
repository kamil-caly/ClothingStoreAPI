using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStoreAPI.Migrations
{
    public partial class fixQuantityName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Orders",
                newName: "ProductQuantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductQuantity",
                table: "Orders",
                newName: "Quantity");
        }
    }
}
