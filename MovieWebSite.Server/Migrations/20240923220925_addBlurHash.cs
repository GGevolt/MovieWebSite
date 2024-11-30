using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieWebSite.Server.Migrations
{
    /// <inheritdoc />
    public partial class addBlurHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilmImg",
                table: "Films",
                newName: "FilmPath");

            migrationBuilder.AddColumn<string>(
                name: "BlurHash",
                table: "Films",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 1,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 2,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 3,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 4,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 5,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 6,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 7,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 8,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 9,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 10,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 11,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 12,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 13,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 14,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 15,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 16,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 17,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 18,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 19,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 20,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 21,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 22,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 23,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 24,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 25,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 26,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 27,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 28,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 29,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 30,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 31,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 32,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 33,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 34,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 35,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 36,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 37,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 38,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 39,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 40,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 41,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 42,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 43,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 44,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 45,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 46,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 47,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 48,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 49,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 50,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 51,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 52,
                column: "BlurHash",
                value: null);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 53,
                column: "BlurHash",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlurHash",
                table: "Films");

            migrationBuilder.RenameColumn(
                name: "FilmPath",
                table: "Films",
                newName: "FilmImg");
        }
    }
}
