using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieWebSite.Server.Migrations.AuthDB
{
    /// <inheritdoc />
    public partial class AddsubcriptionStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionStatus",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8156313e-da30-400d-a83d-646d4bc71dcf", null, "UserT2", "USERT2" },
                    { "b2f2eddd-a561-4ce3-b85e-088af12f5097", null, "Admin", "ADMIN" },
                    { "eee72911-37b8-4be0-a4e1-aae25f4ae1ae", null, "UserT1", "USERT1" },
                    { "f96b655f-a92b-499f-a861-a2634c0b3bad", null, "UserT0", "USERT0" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8156313e-da30-400d-a83d-646d4bc71dcf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2f2eddd-a561-4ce3-b85e-088af12f5097");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eee72911-37b8-4be0-a4e1-aae25f4ae1ae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f96b655f-a92b-499f-a861-a2634c0b3bad");

            migrationBuilder.DropColumn(
                name: "SubscriptionStatus",
                table: "AspNetUsers");

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
    }
}
