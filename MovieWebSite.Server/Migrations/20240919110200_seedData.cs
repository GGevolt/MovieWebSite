using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieWebSite.Server.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilmImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Synopsis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryFilms",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    FilmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFilms", x => new { x.CategoryId, x.FilmId });
                    table.ForeignKey(
                        name: "FK_CategoryFilms_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryFilms_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EpisodeNumber = table.Column<int>(type: "int", nullable: false),
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    VidName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Comedy" },
                    { 3, "Horror" },
                    { 4, "Drama" },
                    { 5, "Thriller" },
                    { 6, "Science Fiction" },
                    { 7, "Romance" },
                    { 8, "Animation" },
                    { 9, "Fantasy" },
                    { 10, "Adventure" },
                    { 11, "Historical" },
                    { 12, "Shonen" },
                    { 13, "Sport" },
                    { 14, "Musical" },
                    { 15, "Idol" },
                    { 16, "Game" },
                    { 17, "Martial Arts" },
                    { 18, "Music" },
                    { 19, "War" },
                    { 20, "Western" },
                    { 21, "Mecha" },
                    { 22, "Crime" },
                    { 23, "Politics" },
                    { 24, "Reality" },
                    { 25, "Biography" },
                    { 26, "Horror" },
                    { 27, "Mystery" },
                    { 28, "Adventure" }
                });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Director", "FilmImg", "Synopsis", "Title", "Type" },
                values: new object[,]
                {
                    { 1, "Masayuki Kojima", "Made_in_Abyss_volume_1_cover.jpg", "The story takes place in the fictional town of Orth, where humans are drawn to a mysterious pit known as the \"Abyss\". The series follows Riko, a young orphan, who becomes obsessed with exploring the Abyss, despite the danger and unknown terrors that lurk within. Alongside her companion, Reg, they delve into the depths of the Abyss, uncovering secrets and facing challenges that threaten their lives. The series explores themes of discovery, friendship, and the human condition.", "Made in Abyss", "TV-Series" },
                    { 2, "Shawn Levy", "2dc59b7ae3022ab3ea57c1d6ad94399a.jpg", "A listless Wade Wilson toils away in civilian life with his days as the morally flexible mercenary, Deadpool, behind him. But when his homeworld faces an existential threat, Wade must reluctantly suit-up again with an even more reluctant Wolverine.", "Deadpool & Wolverine", "Movie" },
                    { 3, "Christopher Nolan", "MV5BMjExMjkwNTQ0Nl5BMl5BanBnXkFtZTcwNTY0OTk1Mw@@._V1_.jpg", "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.", "Inception", "Movie" },
                    { 4, "Ridley Scott", "Gladiator_(2000_film_poster).png", "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.", "Gladiator", "Movie" },
                    { 5, "Hayao Miyazaki", "MV5BMjlmZmI5MDctNDE2YS00YWE0LWE5ZWItZDBhYWQ0NTcxNWRhXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_.jpg", "During her family's move to the suburbs, a sullen 10-year-old girl wanders into a world ruled by gods, witches, and spirits, and where humans are changed into beasts.", "Spirited Away", "Movie" },
                    { 6, "Christopher Nolan", "MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_.jpg", "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.", "The Dark Knight", "Movie" },
                    { 7, "Damien Chazelle", "MV5BMzUzNDM2NzM2MV5BMl5BanBnXkFtZTgwNTM3NTg4OTE@._V1_.jpg", "While navigating their careers in Los Angeles, a pianist and an actress fall in love while attempting to reconcile their aspirations for the future.", "La La Land", "Movie" },
                    { 8, "The Duffer Brothers", "image5.jpg", "When a young boy disappears, his mother, a police chief, and his friends must confront terrifying supernatural forces in order to get him back.", "Stranger Things", "TV-Series" },
                    { 9, "Jon Favreau", "var-www-share-shop-live-src-media-import-iadx4-095-the-mandalorian-gunslingers-web.jpg", "The travels of a lone bounty hunter in the outer reaches of the galaxy, far from the authority of the New Republic.", "The Mandalorian", "TV-Series" },
                    { 10, "Bong Joon-ho", "MV5BYWZjMjk3ZTItODQ2ZC00NTY5LWE0ZDYtZTI3MjcwN2Q5NTVkXkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_.jpg", "Greed and class discrimination threaten the newly formed symbiotic relationship between the wealthy Park family and the destitute Kim clan.", "Parasite", "Movie" },
                    { 11, "Lauren Schmidt Hissrich", "p17580215_b_v13_ab.jpg", "Geralt of Rivia, a mutated monster hunter for hire, journeys toward his destiny in a turbulent world where people often prove more wicked than beasts.", "The Witcher", "TV-Series" },
                    { 12, "Anthony and Joe Russo", "MV5BMTc5MDE2ODcwNV5BMl5BanBnXkFtZTgwMzI2NzQ2NzM@._V1_.jpg", "After the devastating events of Avengers: Infinity War, the universe is in ruins. With the help of remaining allies, the Avengers assemble once more to reverse Thanos' actions and restore balance to the universe.", "Avengers: Endgame", "Movie" },
                    { 13, "Vince Gilligan", "MV5BYmQ4YWMxYjUtNjZmYi00MDQ1LWFjMjMtNjA5ZDdiYjdiODU5XkEyXkFqcGdeQXVyMTMzNDExODE5._V1_.jpg", "A high school chemistry teacher turned methamphetamine manufacturer partners with a former student in a gradual descent into the criminal underworld.", "Breaking Bad", "TV-Series" },
                    { 14, "James Cameron", "MV5BZDA0OGQxNTItMDZkMC00N2UyLTg3MzMtYTJmNjg3Nzk5MzRiXkEyXkFqcGdeQXVyMjUzOTY1NTc@._V1_FMjpg_UX1000_.jpg", "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.", "Avatar", "Movie" },
                    { 15, "David Benioff, D.B. Weiss", "MV5BN2IzYzBiOTQtNGZmMi00NDI5LTgxMzMtN2EzZjA1NjhlOGMxXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_.jpg", "Nine noble families wage war against each other in order to gain control over the mythical land of Westeros.", "Game of Thrones", "TV-Series" },
                    { 16, "The Wachowskis", "613ypTLZHsL._AC_UF894,1000_QL80_.jpg", "A computer hacker learns about the true nature of his reality and his role in the war against its controllers.", "The Matrix", "Movie" },
                    { 17, "Mark Gatiss, Steven Moffat", "MV5BNTQzNGZjNDEtOTMwYi00MzFjLWE2ZTYtYzYxYzMwMjZkZDc5XkEyXkFqcGc@._V1_.jpg", "A modern update finds the famous sleuth and his doctor partner solving crime in 21st century London.", "Sherlock", "TV-Series" },
                    { 18, "George Miller", "p10854488_p_v8_ac.jpg", "In a post-apocalyptic wasteland, Max teams up with a mysterious woman, Furiosa, to try and survive.", "Mad Max: Fury Road", "Movie" },
                    { 19, "Peter Morgan", "71f97nf-u9L._AC_UF1000,1000_QL80_.jpg", "Follows the political rivalries and romance of Queen Elizabeth II's reign and the events that shaped the second half of the 20th century.", "The Crown", "TV-Series" },
                    { 20, "Christopher Nolan", "MV5BZjdkOTU3MDktN2IxOS00OGEyLWFmMjktY2FiMmZkNWIyODZiXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_.jpg", "A team of explorers travels through a wormhole in space in an attempt to ensure humanity's survival.", "Interstellar", "Movie" },
                    { 21, "Tetsurō Araki", "p10701949_b_v8_ah.jpg", "After his hometown is destroyed and his mother is killed, young Eren Jaeger vows to cleanse the earth of the giant humanoid Titans that have brought humanity to the brink of extinction.", "Attack on Titan", "TV-Series" },
                    { 22, "Masayuki Kojima", "unnamed.jpg", "Riko, Reg, and their new friend, Nanachi, continue their journey down the Abyss and arrive at the 5th layer. But in order for them to continue to the 6th layer, they must encounter the haunting figure of Nanachi's past: Bondrewd the Novel.", "Made In Abyss: Dawn of The Deep Soul", "Movie" },
                    { 23, "Hayato Date", "MV5BZmQ5NGFiNWEtMmMyMC00MDdiLTg4YjktOGY5Yzc2MDUxMTE1XkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_FMjpg_UX1000_.jpg", "Follows Naruto Uzumaki, a young ninja with dreams of becoming the strongest ninja and leader of his village.", "Naruto", "TV-Series" },
                    { 24, "Hayao Miyazaki", "MV5BYWM3MDE3YjEtMzIzZC00ODE5LTgxNTItNmUyMTBkM2M2NmNiXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg", "When two girls move to the countryside to be near their ailing mother, they have adventures with the wondrous forest spirits who live nearby.", "My Neighbor Totoro", "Movie" },
                    { 25, "Haruo Sotozaki", "MV5BYTIxNjk3YjItYmYzMC00ZTdmLTk0NGUtZmNlZTA0NWFkZDMwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_.jpg", "A young boy becomes a demon slayer to avenge his family and cure his sister.", "Demon Slayer: Kimetsu no Yaiba", "TV-Series" }
                });

            migrationBuilder.InsertData(
                table: "CategoryFilms",
                columns: new[] { "CategoryId", "FilmId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 6 },
                    { 1, 12 },
                    { 1, 16 },
                    { 1, 18 },
                    { 1, 21 },
                    { 1, 23 },
                    { 1, 25 },
                    { 2, 2 },
                    { 4, 4 },
                    { 4, 8 },
                    { 4, 10 },
                    { 4, 11 },
                    { 4, 13 },
                    { 4, 15 },
                    { 4, 17 },
                    { 4, 19 },
                    { 4, 24 },
                    { 5, 6 },
                    { 5, 8 },
                    { 5, 12 },
                    { 5, 15 },
                    { 5, 18 },
                    { 5, 20 },
                    { 5, 21 },
                    { 6, 1 },
                    { 6, 2 },
                    { 6, 3 },
                    { 6, 8 },
                    { 6, 9 },
                    { 6, 12 },
                    { 6, 14 },
                    { 6, 16 },
                    { 6, 18 },
                    { 6, 20 },
                    { 6, 21 },
                    { 7, 7 },
                    { 8, 1 },
                    { 8, 5 },
                    { 8, 21 },
                    { 8, 22 },
                    { 8, 23 },
                    { 8, 24 },
                    { 8, 25 },
                    { 9, 1 },
                    { 9, 5 },
                    { 9, 9 },
                    { 9, 11 },
                    { 9, 14 },
                    { 9, 15 },
                    { 9, 22 },
                    { 9, 23 },
                    { 9, 24 },
                    { 9, 25 },
                    { 10, 3 },
                    { 10, 5 },
                    { 10, 9 },
                    { 10, 11 },
                    { 10, 14 },
                    { 10, 20 },
                    { 10, 22 },
                    { 10, 23 },
                    { 10, 25 },
                    { 11, 4 },
                    { 11, 19 },
                    { 14, 7 },
                    { 17, 22 },
                    { 17, 24 },
                    { 18, 7 },
                    { 19, 4 },
                    { 22, 6 },
                    { 22, 10 },
                    { 22, 13 },
                    { 22, 17 },
                    { 23, 19 },
                    { 27, 3 },
                    { 27, 10 },
                    { 27, 16 },
                    { 27, 17 },
                    { 28, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFilms_FilmId",
                table: "CategoryFilms",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_FilmId",
                table: "Episodes",
                column: "FilmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryFilms");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Films");
        }
    }
}
