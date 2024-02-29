using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Foodico.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class eeerfds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImageUrl",
                value: "/cake-main/img/shop/product-10.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageUrl",
                value: "wwwroot/cake-main/img/shop/product-1.jpg");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 3, "Cupcake", "", "wwwroot/cake-main/img/shop/product-11.jpg", "Chocolate Cherry", 10.99 },
                    { 4, "Cupcake", "", "wwwroot/cake-main/img/shop/product-12.jpg", "Cherry Cream", 16.989999999999998 },
                    { 5, "Cupcake", "", "wwwroot/cake-main/img/shop/product-2.jpg", "Chocolate Brulee", 21.989999999999998 },
                    { 6, "Cupcake", "", "wwwroot/cake-main/img/shop/product-3.jpg", "Double Chocolate", 11.99 },
                    { 7, "Cupcake", "", "wwwroot/cake-main/img/shop/product-4.jpg", "Pink Donut", 16.949999999999999 },
                    { 8, "Cupcake", "", "wwwroot/cake-main/img/shop/product-5.jpg", "Strawberry Mint", 16.949999999999999 },
                    { 9, "Cupcake", "", "wwwroot/cake-main/img/shop/product-6.jpg", "Forest Berry", 17.949999999999999 },
                    { 10, "Cupcake", "", "wwwroot/cake-main/img/shop/product-7.jpg", "Valentine Velvet", 24.949999999999999 },
                    { 11, "Cupcake", "", "wwwroot/cake-main/img/shop/product-8.jpg", "Strawberry Sprinkle", 24.949999999999999 },
                    { 12, "Cupcake", "", "wwwroot/cake-main/img/shop/product-9.jpg", "Pink Cream", 22.949999999999999 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImageUrl",
                value: "wwwroot\\cake-main\\img\\shop\\product-10.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageUrl",
                value: "wwwroot\\cake-main\\img\\shop\\product-1.jpg");
        }
    }
}
