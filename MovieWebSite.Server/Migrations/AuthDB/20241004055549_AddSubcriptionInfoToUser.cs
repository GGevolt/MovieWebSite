using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieWebSite.Server.Migrations.AuthDB
{
    /// <inheritdoc />
    public partial class AddSubcriptionInfoToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PriceId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionEndPeriod",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "18ea3f4c-4625-4df3-a65c-afc5938db819", null, "UserT0", "USERT0" },
                    { "36add57e-5b5f-4800-b31f-7f3e521a4ef7", null, "UserT1", "USERT1" },
                    { "988f1b18-774b-49fb-a74c-c89067f99950", null, "UserT2", "USERT2" },
                    { "9d57503b-b39e-4396-b973-03c4f68f82f1", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18ea3f4c-4625-4df3-a65c-afc5938db819");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36add57e-5b5f-4800-b31f-7f3e521a4ef7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "988f1b18-774b-49fb-a74c-c89067f99950");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d57503b-b39e-4396-b973-03c4f68f82f1");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SubscriptionEndPeriod",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
