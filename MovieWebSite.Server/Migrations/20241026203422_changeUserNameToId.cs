using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieWebSite.Server.Migrations
{
    /// <inheritdoc />
    public partial class changeUserNameToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "UserFilms",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserFilms",
                newName: "UserName");
        }
    }
}
