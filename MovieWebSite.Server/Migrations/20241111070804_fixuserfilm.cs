using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieWebSite.Server.Migrations
{
    /// <inheritdoc />
    public partial class fixuserfilm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFilms",
                table: "UserFilms");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserFilms",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFilms",
                table: "UserFilms",
                columns: new[] { "UserId", "FilmId" });


            migrationBuilder.CreateIndex(
                name: "IX_UserFilms_FilmId",
                table: "UserFilms",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFilms_AspNetUsers_UserId",
                table: "UserFilms",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFilms_AspNetUsers_UserId",
                table: "UserFilms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFilms",
                table: "UserFilms");

            migrationBuilder.DropIndex(
                name: "IX_UserFilms_FilmId",
                table: "UserFilms");


            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserFilms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFilms",
                table: "UserFilms",
                column: "FilmId");
        }
    }
}
