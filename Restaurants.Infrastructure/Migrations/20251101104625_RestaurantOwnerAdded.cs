using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantOwnerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ownerId",
                table: "restaurants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(
                @"UPDATE restaurants SET ownerId = '5b81511a-34ec-48cc-b4a5-4e82b068481e'");
            // added a default ownerId for existing records

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_ownerId",
                table: "restaurants",
                column: "ownerId");

            migrationBuilder.AddForeignKey(
                name: "FK_restaurants_AspNetUsers_ownerId",
                table: "restaurants",
                column: "ownerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_restaurants_AspNetUsers_ownerId",
                table: "restaurants");

            migrationBuilder.DropIndex(
                name: "IX_restaurants_ownerId",
                table: "restaurants");

            migrationBuilder.DropColumn(
                name: "ownerId",
                table: "restaurants");
        }
    }
}
