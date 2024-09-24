using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieWebSite.Server.Migrations
{
    /// <inheritdoc />
    public partial class MoreFilmData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Director", "FilmImg", "Synopsis", "Title", "Type" },
                values: new object[,]
                {
                    { 26, "Frank Darabont", "MV5BNDE3ODcxYzMtY2YzZC00NmNlLWJiNDMtZDViZWM2MzIxZDYwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_.jpg", "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", "The Shawshank Redemption", "Movie" },
                    { 27, "Peter Jackson", "MV5BNzA5ZDNlZWMtM2NhNS00NDJjLTk4NDItYTRmY2EwMWZlMTY3XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg", "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.", "The Lord of the Rings: The Return of the King", "Movie" },
                    { 28, "Quentin Tarantino", "MV5BNGNhMDIzZTUtNTBlZi00MTRlLWFjM2ItYzViMjE3YzI5MjljXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg", "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.", "Pulp Fiction", "Movie" },
                    { 29, "Francis Ford Coppola", "MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg", "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", "The Godfather", "Movie" },
                    { 30, "Sidney Lumet", "12_Angry_Men_(1957_film_poster).jpg", "A jury holdout attempts to prevent a miscarriage of justice by forcing his colleagues to reconsider the evidence.", "12 Angry Men", "Movie" },
                    { 31, "Steven Spielberg", "MV5BNDE4OTMxMTctNmRhYy00NWE2LTg3YzItYTk3M2UwOTU5Njg4XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg", "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.", "Schindler's List", "Movie" },
                    { 32, "Peter Jackson", "MV5BN2EyZjM3NzUtNWUzMi00MTgxLWI0NTctMzY4M2VlOTdjZWRiXkEyXkFqcGdeQXVyNDUzOTQ5MjY@._V1_.jpg", "A hobbit, a wizard, a dwarf, an elf, and three men embark on a perilous journey to destroy the One Ring and save Middle-earth from Sauron's darkness.", "The Lord of the Rings: The Fellowship of the Ring", "Movie" },
                    { 33, "Miloš Forman", "MV5BZjA0OWVhOTAtYWQxNi00YzNhLWI4ZjYtNjFjZTEyYjJlNDVlL2ltYWdlL2ltYWdlXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_FMjpg_UX1000_.jpg", "A criminal pleads insanity and is admitted to a mental institution, where he challenges the authority of the strict Nurse Ratched.", "One Flew Over the Cuckoo's Nest", "Movie" },
                    { 34, "Christopher Nolan", "MV5BMTk4ODQzNDY3Ml5BMl5BanBnXkFtZTcwODA0NTM4Nw@@._V1_FMjpg_UX1000_.jpg", "Eight years after the Joker's reign of terror, Batman must face his most formidable foe yet: Bane.", "The Dark Knight Rises", "Movie" },
                    { 35, "Quentin Tarantino", "MV5BOTJiNDEzOWYtMTVjOC00ZjlmLWE0NGMtZmE1OWVmZDQ2OWJhXkEyXkFqcGdeQXVyNTIzOTk5ODM@._V1_.jpg", "In Nazi-occupied France during World War II, a group of Jewish-American guerrilla soldiers known as \"The Basterds\" are chosen specifically to spread fear throughout the Third Reich by scalping and brutally killing Nazis.", "Inglourious Basterds", "Movie" },
                    { 36, "Peter Jackson", "MV5BZGMxZTdjZmYtMmE2Ni00ZTdkLWI5NTgtNjlmMjBiNzU2MmI5XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_FMjpg_UX1000_.jpg", "While Frodo and Sam edge closer to Mordor with the One Ring, Aragorn, Legolas and Gimli search for Merry and Pippin, captured by the enemy.", "The Lord of the Rings: The Two Towers", "Movie" },
                    { 37, "Martin Scorsese", "MV5BY2NkZjEzMDgtN2RjYy00YzM1LWI4ZmQtMjIwYjFjNmI3ZGEwXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg", "The story of Henry Hill and his life in the mob, covering his relationship with his wife Karen and his Mob partners Jimmy Conway and Tommy DeVito in the Italian-American Mafia.", "Goodfellas", "Movie" },
                    { 38, "David Fincher", "MV5BOTUwODM5MTctZjczMi00OTk4LTg3NWUtNmVhMTAzNTNjYjcyXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg", "Two detectives hunt for a serial killer who is using the seven deadly sins as a motif for his murders.", "Se7en", "Movie" },
                    { 39, "David Fincher", "MV5BOTgyOGQ1NDItNGU3Ny00MjU3LTg2YWEtNmEyYjBiMjI1Y2M5XkEyXkFqcGc@._V1_.jpg", "An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much darker.", "Fight Club", "Movie" },
                    { 40, "Irvin Kershner", "71ABa3+0B1L._AC_UF1000,1000_QL80_.jpg", "After the Rebel Alliance's victory on Yavin, Luke Skywalker trains as a Jedi Knight under Yoda while his friends evade the Galactic Empire.", "Star Wars: Episode V - The Empire Strikes Back", "Movie" },
                    { 41, "Jonathan Demme", "MV5BNjNhZTk0ZmEtNjJhMi00YzFlLWE1MmEtYzM1M2ZmMGMwMTU4XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg", "A young FBI agent seeks the advice of imprisoned serial killer Hannibal Lecter to apprehend another serial killer known as \"Buffalo Bill\".", "The Silence of the Lambs", "Movie" },
                    { 42, "Frank Capra", "1-7952587233.jpg", "An angel is sent from Heaven to help a desperately frustrated businessman by showing him what life would have been like if he had never existed.", "It's a Wonderful Life", "Movie" },
                    { 43, "George Lucas", "612h-jwI+EL._AC_UF1000,1000_QL80_.jpg", "Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a wookiee and two droids to save the galaxy from the Empire's world-destroying battle-station, while also attempting to rescue Princess Leia from the evil Darth Vader.", "Star Wars: Episode IV - A New Hope", "Movie" },
                    { 44, "Lilly Wachowski, Lana Wachowski", "MV5BODE0MzZhZTgtYzkwYi00YmI5LThlZWYtOWRmNWE5ODk0NzMxXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg", "Neo and Trinity continue their fight against the machines, but they soon discover that they are not alone in their quest.", "The Matrix: Reloaded", "Movie" },
                    { 45, "Gore Verbinski", "pirates-of-the-caribbean-poster.jpg", "Blacksmith Will Turner teams up with eccentric pirate Captain Jack Sparrow to save his love, Elizabeth Swann, from cursed pirates who become undead skeletons in moonlight.", "Pirates of the Caribbean: The Curse of the Black Pearl", "Movie" },
                    { 46, "Greg Daniels", "91d053F2aKL._AC_UF894,1000_QL80_.jpg", "A mockumentary-style sitcom that follows the employees at Dunder Mifflin.", "The Office", "TV-Series" },
                    { 47, "David Crane, Marta Kauffman", "MV5BNDVkYjU0MzctMWRmZi00NTkxLTgwZWEtOWVhYjZlYjllYmU4XkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_FMjpg_UX1000_.jpg", "Follows the personal and professional lives of six friends living in Manhattan.", "Friends", "TV-Series" },
                    { 48, "Chuck Lorre, Bill Prady", "the-big-bang-theory-the-poster-collection-9781608876136_hr.jpg", "A comedy about brilliant physicists Leonard and Sheldon who are socially awkward but intellectually brilliant.", "The Big Bang Theory", "TV-Series" },
                    { 49, "Dan Goor, Michael Schur", "MV5BNzVkYWY4NzYtMWFlZi00YzkwLThhZDItZjcxYTU4ZTMzMDZmXkEyXkFqcGdeQXVyODUxOTU0OTg@._V1_.jpg", "Detective Jake Peralta, a talented and carefree detective with the best arrest record, has never had to follow the rules too closely or work very hard.", "Brooklyn Nine-Nine", "TV-Series" },
                    { 50, "Justin Roiland, Dan Harmon", "MV5BZjRjOTFkOTktZWUzMi00YzMyLThkMmYtMjEwNmQyNzliYTNmXkEyXkFqcGdeQXVyNzQ1ODk3MTQ@._V1_FMjpg_UX1000_.jpg", "An animated series that follows the misadventures of Rick Sanchez, a cynical mad scientist, and his good-hearted grandson Morty Smith.", "Rick and Morty", "TV-Series" },
                    { 51, "Michael Schur", "p12900282_b_v8_aa.jpg", "After her death, Eleanor Shellstrop finds herself in the \"good place\" (heaven) due to a case of mistaken identity.", "The Good Place", "TV-Series" },
                    { 52, "Jonathan Nolan, Lisa Joy", "MV5BZDg1OWRiMTktZDdiNy00NTZlLTg2Y2EtNWRiMTcxMGE5YTUxXkEyXkFqcGdeQXVyMTM2MDY0OTYx._V1_.jpg", "Set at the intersection of the near future and the reimagined past, it explores a world in which every human appetite, no matter how noble or depraved, can be indulged.", "Westworld", "TV-Series" },
                    { 53, "Craig Mazin", "MV5BOTJiOWExNWEtYzE1ZS00NDJmLWFiY2ItYTNjMWZiNTQ2ZDIwXkEyXkFqcGc@._V1_.jpg", "In April 1986, an explosion at the Chernobyl nuclear power plant in the Union of Soviet Socialist Republics becomes one of the world's worst man-made catastrophes.", "Chernobyl", "TV-Series" }
                });

            migrationBuilder.InsertData(
                table: "CategoryFilms",
                columns: new[] { "CategoryId", "FilmId" },
                values: new object[,]
                {
                    { 1, 34 },
                    { 1, 45 },
                    { 2, 46 },
                    { 2, 47 },
                    { 2, 48 },
                    { 2, 49 },
                    { 4, 26 },
                    { 4, 30 },
                    { 4, 42 },
                    { 5, 38 },
                    { 6, 44 },
                    { 8, 50 },
                    { 9, 27 },
                    { 9, 32 },
                    { 9, 36 },
                    { 9, 40 },
                    { 9, 43 },
                    { 9, 52 },
                    { 10, 27 },
                    { 10, 32 },
                    { 10, 36 },
                    { 10, 40 },
                    { 10, 43 },
                    { 10, 45 },
                    { 11, 31 },
                    { 14, 45 },
                    { 22, 28 },
                    { 22, 29 },
                    { 22, 33 },
                    { 22, 34 },
                    { 22, 35 },
                    { 22, 37 },
                    { 22, 39 },
                    { 25, 53 },
                    { 27, 41 },
                    { 27, 44 },
                    { 27, 51 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 1, 34 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 1, 45 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 2, 46 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 2, 47 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 2, 48 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 2, 49 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 4, 26 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 4, 30 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 4, 42 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 5, 38 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 6, 44 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 8, 50 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 9, 27 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 9, 32 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 9, 36 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 9, 40 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 9, 43 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 9, 52 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 10, 27 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 10, 32 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 10, 36 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 10, 40 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 10, 43 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 10, 45 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 11, 31 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 14, 45 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 22, 28 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 22, 29 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 22, 33 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 22, 34 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 22, 35 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 22, 37 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 22, 39 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 25, 53 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 27, 41 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 27, 44 });

            migrationBuilder.DeleteData(
                table: "CategoryFilms",
                keyColumns: new[] { "CategoryId", "FilmId" },
                keyValues: new object[] { 27, 51 });

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 53);
        }
    }
}
