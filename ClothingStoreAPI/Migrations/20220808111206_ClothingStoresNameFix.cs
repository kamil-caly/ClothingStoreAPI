using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStoreAPI.Migrations
{
    public partial class ClothingStoresNameFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothingStore_Addresses_AddressId",
                table: "ClothingStore");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothingStore_Owner_OwnerId",
                table: "ClothingStore");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ClothingStore_StoreId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreReviews_ClothingStore_StoreId",
                table: "StoreReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Owner",
                table: "Owner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClothingStore",
                table: "ClothingStore");

            migrationBuilder.RenameTable(
                name: "Owner",
                newName: "Owners");

            migrationBuilder.RenameTable(
                name: "ClothingStore",
                newName: "ClothingStores");

            migrationBuilder.RenameIndex(
                name: "IX_ClothingStore_OwnerId",
                table: "ClothingStores",
                newName: "IX_ClothingStores_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ClothingStore_AddressId",
                table: "ClothingStores",
                newName: "IX_ClothingStores_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Owners",
                table: "Owners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClothingStores",
                table: "ClothingStores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothingStores_Addresses_AddressId",
                table: "ClothingStores",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothingStores_Owners_OwnerId",
                table: "ClothingStores",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ClothingStores_StoreId",
                table: "Products",
                column: "StoreId",
                principalTable: "ClothingStores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreReviews_ClothingStores_StoreId",
                table: "StoreReviews",
                column: "StoreId",
                principalTable: "ClothingStores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothingStores_Addresses_AddressId",
                table: "ClothingStores");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothingStores_Owners_OwnerId",
                table: "ClothingStores");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ClothingStores_StoreId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreReviews_ClothingStores_StoreId",
                table: "StoreReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Owners",
                table: "Owners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClothingStores",
                table: "ClothingStores");

            migrationBuilder.RenameTable(
                name: "Owners",
                newName: "Owner");

            migrationBuilder.RenameTable(
                name: "ClothingStores",
                newName: "ClothingStore");

            migrationBuilder.RenameIndex(
                name: "IX_ClothingStores_OwnerId",
                table: "ClothingStore",
                newName: "IX_ClothingStore_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ClothingStores_AddressId",
                table: "ClothingStore",
                newName: "IX_ClothingStore_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Owner",
                table: "Owner",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClothingStore",
                table: "ClothingStore",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothingStore_Addresses_AddressId",
                table: "ClothingStore",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothingStore_Owner_OwnerId",
                table: "ClothingStore",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ClothingStore_StoreId",
                table: "Products",
                column: "StoreId",
                principalTable: "ClothingStore",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreReviews_ClothingStore_StoreId",
                table: "StoreReviews",
                column: "StoreId",
                principalTable: "ClothingStore",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
