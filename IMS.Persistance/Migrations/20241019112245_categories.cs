using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert the "Mobile" category
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[] { 1, "Mobile" }
            );

            // Insert 10 subcategories (mobile companies)
            migrationBuilder.InsertData(
                table: "subCategories",
                columns: new[] { "SubCategoryId", "Name", "CategoryId" },
                values: new object[,]
                {
                { 1, "Apple", 1 },
                { 2, "Samsung", 1 },
                { 3, "Xiaomi", 1 },
                { 4, "OnePlus", 1 },
                { 5, "Huawei", 1 },
                { 6, "Nokia", 1 },
                { 7, "Google", 1 },
                { 8, "Oppo", 1 },
                { 9, "Vivo", 1 },
                { 10, "Sony", 1 }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the inserted subcategories
            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "SubCategoryId",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
            );

            // Remove the inserted category
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1
            );
        }
    }
}
