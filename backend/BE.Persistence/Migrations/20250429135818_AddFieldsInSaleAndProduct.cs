using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BE.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsInSaleAndProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61c201a5-1175-49a6-b037-67430e8c1e3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ebe942c-fb88-4a8f-b9b8-28f9b4f92ac8");

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Sales",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql("UPDATE \"Sales\" SET \"IsCanceled\" = false WHERE \"IsCanceled\" IS NULL");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sales",
                type: "varchar(128)",
                nullable: false,
                defaultValue: "");
            migrationBuilder.Sql("UPDATE \"Sales\" SET \"Name\" = 'Venda' WHERE \"Name\" IS NULL");
            migrationBuilder.AddColumn<bool>(
                name: "IsRemovedFromStock",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);
            migrationBuilder.Sql($"UPDATE \"Products\" SET \"IsRemovedFromStock\" = false WHERE \"IsRemovedFromStock\" IS NULL");
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1485ec6c-c4a8-467e-88cd-0782418c43ba", "5144ca22-71b1-4b3d-a695-a2f2015aa9e7", "Admin", "ADMIN" },
                    { "be857a68-51b4-43b7-8183-2a8291940689", "94a1caf9-e7b1-47b6-be8b-813d65f43b72", "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1485ec6c-c4a8-467e-88cd-0782418c43ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be857a68-51b4-43b7-8183-2a8291940689");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsRemovedFromStock",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "61c201a5-1175-49a6-b037-67430e8c1e3c", "7eb46fff-fb29-4a8e-81fb-9ce6aa664f31", "Admin", "ADMIN" },
                    { "7ebe942c-fb88-4a8f-b9b8-28f9b4f92ac8", "e396eeef-bd0c-4d94-b2fa-5698a82dfdf0", "Customer", "CUSTOMER" }
                });
        }
    }
}
