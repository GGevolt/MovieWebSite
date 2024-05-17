using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieWebSite.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeadTrailerEpisode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoQualities_Videos_VideoId",
                table: "VideoQualities");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoQualities",
                table: "VideoQualities");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "VideoQualities",
                newName: "EpisodeId");

            migrationBuilder.AddColumn<int>(
                name: "TrailerId",
                table: "VideoQualities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoQualities",
                table: "VideoQualities",
                columns: new[] { "TrailerId", "EpisodeId", "QualityId" });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EpisodeNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trailers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trailers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoQualities_EpisodeId",
                table: "VideoQualities",
                column: "EpisodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoQualities_Episodes_EpisodeId",
                table: "VideoQualities",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoQualities_Trailers_TrailerId",
                table: "VideoQualities",
                column: "TrailerId",
                principalTable: "Trailers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoQualities_Episodes_EpisodeId",
                table: "VideoQualities");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoQualities_Trailers_TrailerId",
                table: "VideoQualities");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Trailers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoQualities",
                table: "VideoQualities");

            migrationBuilder.DropIndex(
                name: "IX_VideoQualities_EpisodeId",
                table: "VideoQualities");

            migrationBuilder.DropColumn(
                name: "TrailerId",
                table: "VideoQualities");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Films");

            migrationBuilder.RenameColumn(
                name: "EpisodeId",
                table: "VideoQualities",
                newName: "VideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoQualities",
                table: "VideoQualities",
                columns: new[] { "VideoId", "QualityId" });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_VideoQualities_Videos_VideoId",
                table: "VideoQualities",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
