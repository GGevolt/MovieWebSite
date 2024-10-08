using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieWebSite.Server.Migrations.AuthDB
{
    /// <inheritdoc />
    public partial class addCustomerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2697c338-140d-402a-b694-ff44ad681c49", null, "UserT1", "USERT1" },
                    { "29ceb310-e519-4079-852c-32b7bd1cef6c", null, "UserT0", "USERT0" },
                    { "62259732-624e-40ad-9b75-47043ab2f8cb", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2697c338-140d-402a-b694-ff44ad681c49");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29ceb310-e519-4079-852c-32b7bd1cef6c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62259732-624e-40ad-9b75-47043ab2f8cb");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "AspNetUsers");

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
    }
}
