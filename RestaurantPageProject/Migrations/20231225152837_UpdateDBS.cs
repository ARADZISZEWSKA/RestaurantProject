using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantPageProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Wegańskie");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Bezglutenowe");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Bez cukru");

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Puszysty croissant wegański z nadzieniem z malin.", "Croissant z malinami", 22.0 });

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Delikatny croissant bezglutenowy z serkiem i wędzonym łososiem.", "Croissant z serkiem i łososiem", 20.0 });

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Klasyczna bułeczka bez dodatku cukru, obsypana chrupkimi pistacjami.", 18.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Croissanty słodkie");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Croissanty słone");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Bułeczki");

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Puszyste croissanty nadziewane kremowym mascarpone i świeżymi malinami.", "Croissanty słodkie z mascarpone i malinami", 20.0 });

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Delikatne croissanty z serkiem i wędzonym łososiem.", "Croissanty słone z serkiem i łososiem", 18.0 });

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Klasyczna bułeczka obsypana chrupkimi pistacjami.", 15.0 });
        }
    }
}
