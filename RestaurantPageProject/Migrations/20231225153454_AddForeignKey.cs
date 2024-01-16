using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantPageProject.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Menu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_CategoryId",
                table: "Menu",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Categories_CategoryId",
                table: "Menu",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Categories_CategoryId",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_CategoryId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Menu");
        }
    }
}
