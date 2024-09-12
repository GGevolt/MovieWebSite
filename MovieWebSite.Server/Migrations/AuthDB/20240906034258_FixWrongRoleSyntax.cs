using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieWebSite.Server.Migrations.AuthDB
{
    /// <inheritdoc />
    public partial class FixWrongRoleSyntax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "794fc0af-5be7-4889-9f20-d054a870dc18");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5311451-61ee-476f-a2c2-5bfaa6827153");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc86ab9c-77ad-4454-a2b0-44d9bc56088f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3cb0b449-836c-4875-82b9-e032a144e46c", null, "UserT1", "USERT1" },
                    { "a78a2db5-ca4c-4d72-b865-ff08aa3741a2", null, "UserT0", "USERT0" },
                    { "e895a8e7-1cf3-449b-9b42-a80209eab8f4", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cb0b449-836c-4875-82b9-e032a144e46c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a78a2db5-ca4c-4d72-b865-ff08aa3741a2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e895a8e7-1cf3-449b-9b42-a80209eab8f4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "794fc0af-5be7-4889-9f20-d054a870dc18", null, "UserT1", "USERT1" },
                    { "b5311451-61ee-476f-a2c2-5bfaa6827153", null, "UserT0", "USER0" },
                    { "fc86ab9c-77ad-4454-a2b0-44d9bc56088f", null, "Admin", "ADMIN" }
                });
        }
    }
}
