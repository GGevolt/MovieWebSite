using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieWebSite.Server.Migrations
{
    /// <inheritdoc />
    public partial class changevaluename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Episodes_EpisodeId",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "VidUrl",
                table: "Videos",
                newName: "VidName");

            migrationBuilder.AlterColumn<int>(
                name: "EpisodeId",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Episodes_EpisodeId",
                table: "Videos",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Episodes_EpisodeId",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "VidName",
                table: "Videos",
                newName: "VidUrl");

            migrationBuilder.AlterColumn<int>(
                name: "EpisodeId",
                table: "Videos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Episodes_EpisodeId",
                table: "Videos",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id");
        }
    }
}
