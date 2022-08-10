using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStoreAPI.Migrations
{
    public partial class UserCreatedByAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "StoreReviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "ProductReviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "ClothingStores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Baskets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreReviews_CreatedById",
                table: "StoreReviews",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedById",
                table: "Products",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_CreatedById",
                table: "ProductReviews",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClothingStores_CreatedById",
                table: "ClothingStores",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_CreatedById",
                table: "Baskets",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Users_CreatedById",
                table: "Baskets",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothingStores_Users_CreatedById",
                table: "ClothingStores",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_Users_CreatedById",
                table: "ProductReviews",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedById",
                table: "Products",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreReviews_Users_CreatedById",
                table: "StoreReviews",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Users_CreatedById",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothingStores_Users_CreatedById",
                table: "ClothingStores");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_Users_CreatedById",
                table: "ProductReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CreatedById",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreReviews_Users_CreatedById",
                table: "StoreReviews");

            migrationBuilder.DropIndex(
                name: "IX_StoreReviews_CreatedById",
                table: "StoreReviews");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedById",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductReviews_CreatedById",
                table: "ProductReviews");

            migrationBuilder.DropIndex(
                name: "IX_ClothingStores_CreatedById",
                table: "ClothingStores");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_CreatedById",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "StoreReviews");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ProductReviews");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ClothingStores");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Baskets");
        }
    }
}
