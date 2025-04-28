using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BE.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "61c201a5-1175-49a6-b037-67430e8c1e3c", "7eb46fff-fb29-4a8e-81fb-9ce6aa664f31", "Admin", "ADMIN" },
                    { "7ebe942c-fb88-4a8f-b9b8-28f9b4f92ac8", "e396eeef-bd0c-4d94-b2fa-5698a82dfdf0", "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61c201a5-1175-49a6-b037-67430e8c1e3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ebe942c-fb88-4a8f-b9b8-28f9b4f92ac8");
        }
    }
}
