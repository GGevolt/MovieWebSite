using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Model.AuthModels;
using Server.Model.Models;

namespace MovieWebSite.Server.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<CategoryFilm> CategoryFilms { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserFilm> UserFilms { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option) : base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().HasIndex(u => u.UserName).IsUnique();

            modelBuilder.Entity<UserFilm>()
                .HasKey(uf => new { uf.UserId, uf.FilmId });

            modelBuilder.Entity<UserFilm>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.UserFilms)
                .HasForeignKey(uf => uf.UserId);

            modelBuilder.Entity<UserFilm>()
                .HasOne(uf => uf.Film)
                .WithMany(f => f.UserFilms)
                .HasForeignKey(uf => uf.FilmId);

            modelBuilder.Entity<CategoryFilm>()
                .HasKey(cf => new { cf.CategoryId, cf.FilmId });

            modelBuilder.Entity<CategoryFilm>()
                .HasOne(cf => cf.Category)
                .WithMany(c => c.CategoryFilms)
                .HasForeignKey(cf => cf.CategoryId);

            modelBuilder.Entity<CategoryFilm>()
                .HasOne(cf => cf.Film)
                .WithMany(f => f.CategoryFilms)
                .HasForeignKey(cf => cf.FilmId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "UserT0",
                    NormalizedName = "USERT0"
                },
                new IdentityRole
                {
                    Name = "UserT1",
                    NormalizedName = "USERT1"
                },
                new IdentityRole
                {
                    Name = "UserT2",
                    NormalizedName = "USERT2"
                },
            };
            foreach (var role in roles)
            {
                modelBuilder.Entity<IdentityRole>()
                    .HasData(role);
            }

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action" },
                new Category { Id = 2, Name = "Comedy" },
                new Category { Id = 3, Name = "Horror" },
                new Category { Id = 4, Name = "Drama" },
                new Category { Id = 5, Name = "Thriller" },
                new Category { Id = 6, Name = "Science Fiction" },
                new Category { Id = 7, Name = "Romance" },
                new Category { Id = 8, Name = "Animation" },
                new Category { Id = 9, Name = "Fantasy" },
                new Category { Id = 10, Name = "Adventure" },
                new Category { Id = 11, Name = "Historical" },
                new Category { Id = 12, Name = "Shonen" },
                new Category { Id = 13, Name = "Sport" },
                new Category { Id = 14, Name = "Musical" },
                new Category { Id = 15, Name = "Idol" },
                new Category { Id = 16, Name = "Game" },
                new Category { Id = 17, Name = "Martial Arts" },
                new Category { Id = 18, Name = "Music" },
                new Category { Id = 19, Name = "War" },
                new Category { Id = 20, Name = "Western" },
                new Category { Id = 21, Name = "Mecha" },
                new Category { Id = 22, Name = "Crime" },
                new Category { Id = 23, Name = "Politics" },
                new Category { Id = 24, Name = "Reality" },
                new Category { Id = 25, Name = "Biography" },
                new Category { Id = 26, Name = "Horror" },
                new Category { Id = 27, Name = "Mystery" },
                new Category { Id = 28, Name = "Adventure" }
            );
            modelBuilder.Entity<Film>().HasData(
                new Film { 
                    Id = 1,
                    Title= "Made in Abyss", 
                    Type = "TV-Series", 
                    Director= "Masayuki Kojima", 
                    Synopsis = "The story takes place in the fictional town of Orth, where humans are drawn to a mysterious pit known as the \"Abyss\". The series follows Riko, a young orphan, who becomes obsessed with exploring the Abyss, despite the danger and unknown terrors that lurk within. Alongside her companion, Reg, they delve into the depths of the Abyss, uncovering secrets and facing challenges that threaten their lives. The series explores themes of discovery, friendship, and the human condition.",
                    FilmPath = "Made_in_Abyss_volume_1_cover.jpg",
                }, 
                new Film
                {
                    Id = 2,
                    Title = "Deadpool & Wolverine",
                    Type = "Movie",
                    Director = "Shawn Levy",
                    Synopsis = "A listless Wade Wilson toils away in civilian life with his days as the morally flexible mercenary, Deadpool, behind him. But when his homeworld faces an existential threat, Wade must reluctantly suit-up again with an even more reluctant Wolverine.",
                    FilmPath = "2dc59b7ae3022ab3ea57c1d6ad94399a.jpg",
                }, 
                new Film
                {
                    Id = 3,
                    Title = "Inception",
                    Type = "Movie",
                    Director = "Christopher Nolan",
                    Synopsis = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.",
                    FilmPath = "MV5BMjExMjkwNTQ0Nl5BMl5BanBnXkFtZTcwNTY0OTk1Mw@@._V1_.jpg"
                },
                new Film
                {
                    Id = 4,
                    Title = "Gladiator",
                    Type = "Movie",
                    Director = "Ridley Scott",
                    Synopsis = "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.",
                    FilmPath = "Gladiator_(2000_film_poster).png"
                },
                new Film
                {
                    Id = 5,
                    Title = "Spirited Away",
                    Type = "Movie",
                    Director = "Hayao Miyazaki",
                    Synopsis = "During her family's move to the suburbs, a sullen 10-year-old girl wanders into a world ruled by gods, witches, and spirits, and where humans are changed into beasts.",
                    FilmPath = "MV5BMjlmZmI5MDctNDE2YS00YWE0LWE5ZWItZDBhYWQ0NTcxNWRhXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_.jpg"
                },
                new Film
                {
                    Id = 6,
                    Title = "The Dark Knight",
                    Type = "Movie",
                    Director = "Christopher Nolan",
                    Synopsis = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                    FilmPath = "MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_.jpg"
                },
                new Film
                {
                    Id = 7,
                    Title = "La La Land",
                    Type = "Movie",
                    Director = "Damien Chazelle",
                    Synopsis = "While navigating their careers in Los Angeles, a pianist and an actress fall in love while attempting to reconcile their aspirations for the future.",
                    FilmPath = "MV5BMzUzNDM2NzM2MV5BMl5BanBnXkFtZTgwNTM3NTg4OTE@._V1_.jpg"
                },
                new Film
                {
                    Id = 8,
                    Title = "Stranger Things",
                    Type = "TV-Series",
                    Director = "The Duffer Brothers",
                    Synopsis = "When a young boy disappears, his mother, a police chief, and his friends must confront terrifying supernatural forces in order to get him back.",
                    FilmPath = "image5.jpg"
                },
                new Film
                {
                    Id = 9,
                    Title = "The Mandalorian",
                    Type = "TV-Series",
                    Director = "Jon Favreau",
                    Synopsis = "The travels of a lone bounty hunter in the outer reaches of the galaxy, far from the authority of the New Republic.",
                    FilmPath = "var-www-share-shop-live-src-media-import-iadx4-095-the-mandalorian-gunslingers-web.jpg"
                },
                new Film
                {
                    Id = 10,
                    Title = "Parasite",
                    Type = "Movie",
                    Director = "Bong Joon-ho",
                    Synopsis = "Greed and class discrimination threaten the newly formed symbiotic relationship between the wealthy Park family and the destitute Kim clan.",
                    FilmPath = "MV5BYWZjMjk3ZTItODQ2ZC00NTY5LWE0ZDYtZTI3MjcwN2Q5NTVkXkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_.jpg"
                },
                new Film
                {
                    Id = 11,
                    Title = "The Witcher",
                    Type = "TV-Series",
                    Director = "Lauren Schmidt Hissrich",
                    Synopsis = "Geralt of Rivia, a mutated monster hunter for hire, journeys toward his destiny in a turbulent world where people often prove more wicked than beasts.",
                    FilmPath = "p17580215_b_v13_ab.jpg"
                },
                new Film
                {
                    Id = 12,
                    Title = "Avengers: Endgame",
                    Type = "Movie",
                    Director = "Anthony and Joe Russo",
                    Synopsis = "After the devastating events of Avengers: Infinity War, the universe is in ruins. With the help of remaining allies, the Avengers assemble once more to reverse Thanos' actions and restore balance to the universe.",
                    FilmPath = "MV5BMTc5MDE2ODcwNV5BMl5BanBnXkFtZTgwMzI2NzQ2NzM@._V1_.jpg"
                }, new Film
                {
                    Id = 13,
                    Title = "Breaking Bad",
                    Type = "TV-Series",
                    Director = "Vince Gilligan",
                    Synopsis = "A high school chemistry teacher turned methamphetamine manufacturer partners with a former student in a gradual descent into the criminal underworld.",
                    FilmPath = "MV5BYmQ4YWMxYjUtNjZmYi00MDQ1LWFjMjMtNjA5ZDdiYjdiODU5XkEyXkFqcGdeQXVyMTMzNDExODE5._V1_.jpg"
                },
                new Film
                {
                    Id = 14,
                    Title = "Avatar",
                    Type = "Movie",
                    Director = "James Cameron",
                    Synopsis = "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.",
                    FilmPath = "MV5BZDA0OGQxNTItMDZkMC00N2UyLTg3MzMtYTJmNjg3Nzk5MzRiXkEyXkFqcGdeQXVyMjUzOTY1NTc@._V1_FMjpg_UX1000_.jpg"
                },
                new Film
                {
                    Id = 15,
                    Title = "Game of Thrones",
                    Type = "TV-Series",
                    Director = "David Benioff, D.B. Weiss",
                    Synopsis = "Nine noble families wage war against each other in order to gain control over the mythical land of Westeros.",
                    FilmPath = "MV5BN2IzYzBiOTQtNGZmMi00NDI5LTgxMzMtN2EzZjA1NjhlOGMxXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_.jpg"
                },
                new Film
                {
                    Id = 16,
                    Title = "The Matrix",
                    Type = "Movie",
                    Director = "The Wachowskis",
                    Synopsis = "A computer hacker learns about the true nature of his reality and his role in the war against its controllers.",
                    FilmPath = "613ypTLZHsL._AC_UF894,1000_QL80_.jpg"
                },
                new Film
                {
                    Id = 17,
                    Title = "Sherlock",
                    Type = "TV-Series",
                    Director = "Mark Gatiss, Steven Moffat",
                    Synopsis = "A modern update finds the famous sleuth and his doctor partner solving crime in 21st century London.",
                    FilmPath = "MV5BNTQzNGZjNDEtOTMwYi00MzFjLWE2ZTYtYzYxYzMwMjZkZDc5XkEyXkFqcGc@._V1_.jpg"
                },
                new Film
                {
                    Id = 18,
                    Title = "Mad Max: Fury Road",
                    Type = "Movie",
                    Director = "George Miller",
                    Synopsis = "In a post-apocalyptic wasteland, Max teams up with a mysterious woman, Furiosa, to try and survive.",
                    FilmPath = "p10854488_p_v8_ac.jpg"
                },
                new Film
                {
                    Id = 19,
                    Title = "The Crown",
                    Type = "TV-Series",
                    Director = "Peter Morgan",
                    Synopsis = "Follows the political rivalries and romance of Queen Elizabeth II's reign and the events that shaped the second half of the 20th century.",
                    FilmPath = "71f97nf-u9L._AC_UF1000,1000_QL80_.jpg"
                },
                new Film
                {
                    Id = 20,
                    Title = "Interstellar",
                    Type = "Movie",
                    Director = "Christopher Nolan",
                    Synopsis = "A team of explorers travels through a wormhole in space in an attempt to ensure humanity's survival.",
                    FilmPath = "MV5BZjdkOTU3MDktN2IxOS00OGEyLWFmMjktY2FiMmZkNWIyODZiXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_.jpg"
                },
                new Film
                {
                    Id = 21,
                    Title = "Attack on Titan",
                    Type = "TV-Series",
                    Director = "Tetsurō Araki",
                    Synopsis = "After his hometown is destroyed and his mother is killed, young Eren Jaeger vows to cleanse the earth of the giant humanoid Titans that have brought humanity to the brink of extinction.",
                    FilmPath = "p10701949_b_v8_ah.jpg"
                },
                new Film
                {
                    Id = 22,
                    Title = "Made In Abyss: Dawn of The Deep Soul",
                    Type = "Movie",
                    Director = "Masayuki Kojima",
                    Synopsis = "Riko, Reg, and their new friend, Nanachi, continue their journey down the Abyss and arrive at the 5th layer. But in order for them to continue to the 6th layer, they must encounter the haunting figure of Nanachi's past: Bondrewd the Novel.",
                    FilmPath = "unnamed.jpg"
                },
                new Film
                {
                    Id = 23,
                    Title = "Naruto",
                    Type = "TV-Series",
                    Director = "Hayato Date",
                    Synopsis = "Follows Naruto Uzumaki, a young ninja with dreams of becoming the strongest ninja and leader of his village.",
                    FilmPath = "MV5BZmQ5NGFiNWEtMmMyMC00MDdiLTg4YjktOGY5Yzc2MDUxMTE1XkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_FMjpg_UX1000_.jpg"
                },
                new Film
                {
                    Id = 24,
                    Title = "My Neighbor Totoro",
                    Type = "Movie",
                    Director = "Hayao Miyazaki",
                    Synopsis = "When two girls move to the countryside to be near their ailing mother, they have adventures with the wondrous forest spirits who live nearby.",
                    FilmPath = "MV5BYWM3MDE3YjEtMzIzZC00ODE5LTgxNTItNmUyMTBkM2M2NmNiXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg"
                },
                new Film
                {
                    Id = 25,
                    Title = "Demon Slayer: Kimetsu no Yaiba",
                    Type = "TV-Series",
                    Director = "Haruo Sotozaki",
                    Synopsis = "A young boy becomes a demon slayer to avenge his family and cure his sister.",
                    FilmPath = "MV5BYTIxNjk3YjItYmYzMC00ZTdmLTk0NGUtZmNlZTA0NWFkZDMwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_.jpg"
                },
                new Film
                {
                    Id = 26,
                    Title = "The Shawshank Redemption",
                    Type = "Movie",
                    Director = "Frank Darabont",
                    Synopsis = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    FilmPath = "MV5BNDE3ODcxYzMtY2YzZC00NmNlLWJiNDMtZDViZWM2MzIxZDYwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_.jpg"
                },
                new Film
                {
                    Id = 27,
                    Title = "The Lord of the Rings: The Return of the King",
                    Type = "Movie",
                    Director = "Peter Jackson",
                    Synopsis = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                    FilmPath = "MV5BNzA5ZDNlZWMtM2NhNS00NDJjLTk4NDItYTRmY2EwMWZlMTY3XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg"
                },
                new Film
                {
                    Id = 28,
                    Title = "Pulp Fiction",
                    Type = "Movie",
                    Director = "Quentin Tarantino",
                    Synopsis = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    FilmPath = "MV5BNGNhMDIzZTUtNTBlZi00MTRlLWFjM2ItYzViMjE3YzI5MjljXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg"
                },
                new Film
                {
                    Id = 29,
                    Title = "The Godfather",
                    Type = "Movie",
                    Director = "Francis Ford Coppola",
                    Synopsis = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                    FilmPath = "MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg"
                },
                new Film
                {
                    Id = 30,
                    Title = "12 Angry Men",
                    Type = "Movie",
                    Director = "Sidney Lumet",
                    Synopsis = "A jury holdout attempts to prevent a miscarriage of justice by forcing his colleagues to reconsider the evidence.",
                    FilmPath = "12_Angry_Men_(1957_film_poster).jpg"
                },
                new Film
                {
                    Id = 31,
                    Title = "Schindler's List",
                    Type = "Movie",
                    Director = "Steven Spielberg",
                    Synopsis = "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.",
                    FilmPath = "MV5BNDE4OTMxMTctNmRhYy00NWE2LTg3YzItYTk3M2UwOTU5Njg4XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg"
                },
                new Film
                {
                    Id = 32,
                    Title = "The Lord of the Rings: The Fellowship of the Ring",
                    Type = "Movie",
                    Director = "Peter Jackson",
                    Synopsis = "A hobbit, a wizard, a dwarf, an elf, and three men embark on a perilous journey to destroy the One Ring and save Middle-earth from Sauron's darkness.",
                    FilmPath = "MV5BN2EyZjM3NzUtNWUzMi00MTgxLWI0NTctMzY4M2VlOTdjZWRiXkEyXkFqcGdeQXVyNDUzOTQ5MjY@._V1_.jpg"
                },
                new Film
                {
                    Id = 33,
                    Title = "One Flew Over the Cuckoo's Nest",
                    Type = "Movie",
                    Director = "Miloš Forman",
                    Synopsis = "A criminal pleads insanity and is admitted to a mental institution, where he challenges the authority of the strict Nurse Ratched.",
                    FilmPath = "MV5BZjA0OWVhOTAtYWQxNi00YzNhLWI4ZjYtNjFjZTEyYjJlNDVlL2ltYWdlL2ltYWdlXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_FMjpg_UX1000_.jpg"
                },
                new Film
                {
                    Id = 34,
                    Title = "The Dark Knight Rises",
                    Type = "Movie",
                    Director = "Christopher Nolan",
                    Synopsis = "Eight years after the Joker's reign of terror, Batman must face his most formidable foe yet: Bane.",
                    FilmPath = "MV5BMTk4ODQzNDY3Ml5BMl5BanBnXkFtZTcwODA0NTM4Nw@@._V1_FMjpg_UX1000_.jpg"
                },
                new Film
                {
                    Id = 35,
                    Title = "Inglourious Basterds",
                    Type = "Movie",
                    Director = "Quentin Tarantino",
                    Synopsis = "In Nazi-occupied France during World War II, a group of Jewish-American guerrilla soldiers known as \"The Basterds\" are chosen specifically to spread fear throughout the Third Reich by scalping and brutally killing Nazis.",
                    FilmPath = "MV5BOTJiNDEzOWYtMTVjOC00ZjlmLWE0NGMtZmE1OWVmZDQ2OWJhXkEyXkFqcGdeQXVyNTIzOTk5ODM@._V1_.jpg"
                },
                new Film
                {
                    Id = 36,
                    Title = "The Lord of the Rings: The Two Towers",
                    Type = "Movie",
                    Director = "Peter Jackson",
                    Synopsis = "While Frodo and Sam edge closer to Mordor with the One Ring, Aragorn, Legolas and Gimli search for Merry and Pippin, captured by the enemy.",
                    FilmPath = "MV5BZGMxZTdjZmYtMmE2Ni00ZTdkLWI5NTgtNjlmMjBiNzU2MmI5XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_FMjpg_UX1000_.jpg"
                },
                new Film
                {
                    Id = 37,
                    Title = "Goodfellas",
                    Type = "Movie",
                    Director = "Martin Scorsese",
                    Synopsis = "The story of Henry Hill and his life in the mob, covering his relationship with his wife Karen and his Mob partners Jimmy Conway and Tommy DeVito in the Italian-American Mafia.",
                    FilmPath = "MV5BY2NkZjEzMDgtN2RjYy00YzM1LWI4ZmQtMjIwYjFjNmI3ZGEwXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg"
                },
                new Film
                {
                    Id = 38,
                    Title = "Se7en",
                    Type = "Movie",
                    Director = "David Fincher",
                    Synopsis = "Two detectives hunt for a serial killer who is using the seven deadly sins as a motif for his murders.",
                    FilmPath = "MV5BOTUwODM5MTctZjczMi00OTk4LTg3NWUtNmVhMTAzNTNjYjcyXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg"
                },
                new Film
                {
                    Id = 39,
                    Title = "Fight Club",
                    Type = "Movie",
                    Director = "David Fincher",
                    Synopsis = "An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much darker.",
                    FilmPath = "MV5BOTgyOGQ1NDItNGU3Ny00MjU3LTg2YWEtNmEyYjBiMjI1Y2M5XkEyXkFqcGc@._V1_.jpg"
                },
                new Film
                {
                    Id = 40,
                    Title = "Star Wars: Episode V - The Empire Strikes Back",
                    Type = "Movie",
                    Director = "Irvin Kershner",
                    Synopsis = "After the Rebel Alliance's victory on Yavin, Luke Skywalker trains as a Jedi Knight under Yoda while his friends evade the Galactic Empire.",
                    FilmPath = "71ABa3+0B1L._AC_UF1000,1000_QL80_.jpg"
                },
                new Film
                {
                    Id = 41,
                    Title = "The Silence of the Lambs",
                    Type = "Movie",
                    Director = "Jonathan Demme",
                    Synopsis = "A young FBI agent seeks the advice of imprisoned serial killer Hannibal Lecter to apprehend another serial killer known as \"Buffalo Bill\".",
                    FilmPath = "MV5BNjNhZTk0ZmEtNjJhMi00YzFlLWE1MmEtYzM1M2ZmMGMwMTU4XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg"
                },
                new Film
                {
                    Id = 42,
                    Title = "It's a Wonderful Life",
                    Type = "Movie",
                    Director = "Frank Capra",
                    Synopsis = "An angel is sent from Heaven to help a desperately frustrated businessman by showing him what life would have been like if he had never existed.",
                    FilmPath = "1-7952587233.jpg"
                },
                new Film
                {
                    Id = 43,
                    Title = "Star Wars: Episode IV - A New Hope",
                    Type = "Movie",
                    Director = "George Lucas",
                    Synopsis = "Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a wookiee and two droids to save the galaxy from the Empire's world-destroying battle-station, while also attempting to rescue Princess Leia from the evil Darth Vader.",
                    FilmPath = "612h-jwI+EL._AC_UF1000,1000_QL80_.jpg"
                },
                new Film
                {
                    Id = 44,
                    Title = "The Matrix: Reloaded",
                    Type = "Movie",
                    Director = "Lilly Wachowski, Lana Wachowski",
                    Synopsis = "Neo and Trinity continue their fight against the machines, but they soon discover that they are not alone in their quest.",
                    FilmPath = "MV5BODE0MzZhZTgtYzkwYi00YmI5LThlZWYtOWRmNWE5ODk0NzMxXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg"
                },
                new Film
                {
                    Id = 45,
                    Title = "Pirates of the Caribbean: The Curse of the Black Pearl",
                    Type = "Movie",
                    Director = "Gore Verbinski",
                    Synopsis = "Blacksmith Will Turner teams up with eccentric pirate Captain Jack Sparrow to save his love, Elizabeth Swann, from cursed pirates who become undead skeletons in moonlight.",
                    FilmPath = "pirates-of-the-caribbean-poster.jpg"
                },
                new Film
                {
                    Id = 46,
                    Title = "The Office",
                    Type = "TV-Series",
                    Director = "Greg Daniels",
                    Synopsis = "A mockumentary-style sitcom that follows the employees at Dunder Mifflin.",
                    FilmPath = "91d053F2aKL._AC_UF894,1000_QL80_.jpg"
                },
                new Film
                {
                    Id = 47,
                    Title = "Friends",
                    Type = "TV-Series",
                    Director = "David Crane, Marta Kauffman",
                    Synopsis = "Follows the personal and professional lives of six friends living in Manhattan.",
                    FilmPath = "MV5BNDVkYjU0MzctMWRmZi00NTkxLTgwZWEtOWVhYjZlYjllYmU4XkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_FMjpg_UX1000_.jpg"
                },
                new Film
                {
                    Id = 48,
                    Title = "The Big Bang Theory",
                    Type = "TV-Series",
                    Director = "Chuck Lorre, Bill Prady",
                    Synopsis = "A comedy about brilliant physicists Leonard and Sheldon who are socially awkward but intellectually brilliant.",
                    FilmPath = "the-big-bang-theory-the-poster-collection-9781608876136_hr.jpg"
                },
                new Film
                {
                    Id = 49,
                    Title = "Brooklyn Nine-Nine",
                    Type = "TV-Series",
                    Director = "Dan Goor, Michael Schur",
                    Synopsis = "Detective Jake Peralta, a talented and carefree detective with the best arrest record, has never had to follow the rules too closely or work very hard.",
                    FilmPath = "MV5BNzVkYWY4NzYtMWFlZi00YzkwLThhZDItZjcxYTU4ZTMzMDZmXkEyXkFqcGdeQXVyODUxOTU0OTg@._V1_.jpg"
                },
                new Film
                {
                    Id = 50,
                    Title = "Rick and Morty",
                    Type = "TV-Series",
                    Director = "Justin Roiland, Dan Harmon",
                    Synopsis = "An animated series that follows the misadventures of Rick Sanchez, a cynical mad scientist, and his good-hearted grandson Morty Smith.",
                    FilmPath = "MV5BZjRjOTFkOTktZWUzMi00YzMyLThkMmYtMjEwNmQyNzliYTNmXkEyXkFqcGdeQXVyNzQ1ODk3MTQ@._V1_FMjpg_UX1000_.jpg"
                },
                new Film
                {
                    Id = 51,
                    Title = "The Good Place",
                    Type = "TV-Series",
                    Director = "Michael Schur",
                    Synopsis = "After her death, Eleanor Shellstrop finds herself in the \"good place\" (heaven) due to a case of mistaken identity.",
                    FilmPath = "p12900282_b_v8_aa.jpg"
                },
                new Film
                {
                    Id = 52,
                    Title = "Westworld",
                    Type = "TV-Series",
                    Director = "Jonathan Nolan, Lisa Joy",
                    Synopsis = "Set at the intersection of the near future and the reimagined past, it explores a world in which every human appetite, no matter how noble or depraved, can be indulged.",
                    FilmPath = "MV5BZDg1OWRiMTktZDdiNy00NTZlLTg2Y2EtNWRiMTcxMGE5YTUxXkEyXkFqcGdeQXVyMTM2MDY0OTYx._V1_.jpg"
                },
                new Film
                {
                    Id = 53,
                    Title = "Chernobyl",
                    Type = "TV-Series",
                    Director = "Craig Mazin",
                    Synopsis = "In April 1986, an explosion at the Chernobyl nuclear power plant in the Union of Soviet Socialist Republics becomes one of the world's worst man-made catastrophes.",
                    FilmPath = "MV5BOTJiOWExNWEtYzE1ZS00NDJmLWFiY2ItYTNjMWZiNTQ2ZDIwXkEyXkFqcGc@._V1_.jpg"
                }


            );
            modelBuilder.Entity<CategoryFilm>().HasData(
                new CategoryFilm { FilmId = 1, CategoryId = 28 },
                new CategoryFilm { FilmId = 1, CategoryId = 6 },
                new CategoryFilm { FilmId = 1, CategoryId = 9 },
                new CategoryFilm { FilmId = 1, CategoryId = 8 },
                new CategoryFilm { FilmId = 2, CategoryId = 6 },
                new CategoryFilm { FilmId = 2, CategoryId = 1 },
                new CategoryFilm { FilmId = 2, CategoryId = 2 },
                new CategoryFilm { FilmId = 3, CategoryId = 6 },  
                new CategoryFilm { FilmId = 3, CategoryId = 10 }, 
                new CategoryFilm { FilmId = 3, CategoryId = 27 }, 
                new CategoryFilm { FilmId = 4, CategoryId = 11 }, 
                new CategoryFilm { FilmId = 4, CategoryId = 19 }, 
                new CategoryFilm { FilmId = 4, CategoryId = 4 },  
                new CategoryFilm { FilmId = 5, CategoryId = 8 },  
                new CategoryFilm { FilmId = 5, CategoryId = 9 },  
                new CategoryFilm { FilmId = 5, CategoryId = 10 }, 
                new CategoryFilm { FilmId = 6, CategoryId = 1 },  
                new CategoryFilm { FilmId = 6, CategoryId = 22 }, 
                new CategoryFilm { FilmId = 6, CategoryId = 5 },  
                new CategoryFilm { FilmId = 7, CategoryId = 14 }, 
                new CategoryFilm { FilmId = 7, CategoryId = 18 }, 
                new CategoryFilm { FilmId = 7, CategoryId = 7 },
                new CategoryFilm { FilmId = 8, CategoryId = 6 },  
                new CategoryFilm { FilmId = 8, CategoryId = 4 },  
                new CategoryFilm { FilmId = 8, CategoryId = 5 },  
                new CategoryFilm { FilmId = 9, CategoryId = 9 },  
                new CategoryFilm { FilmId = 9, CategoryId = 10 }, 
                new CategoryFilm { FilmId = 9, CategoryId = 6 },  
                new CategoryFilm { FilmId = 10, CategoryId = 4 }, 
                new CategoryFilm { FilmId = 10, CategoryId = 22 },
                new CategoryFilm { FilmId = 10, CategoryId = 27 },
                new CategoryFilm { FilmId = 11, CategoryId = 9 }, 
                new CategoryFilm { FilmId = 11, CategoryId = 4 }, 
                new CategoryFilm { FilmId = 11, CategoryId = 10 },
                new CategoryFilm { FilmId = 12, CategoryId = 1 }, 
                new CategoryFilm { FilmId = 12, CategoryId = 6 }, 
                new CategoryFilm { FilmId = 12, CategoryId = 5 },  
                new CategoryFilm { FilmId = 13, CategoryId = 22 }, 
                new CategoryFilm { FilmId = 13, CategoryId = 4 },  
                new CategoryFilm { FilmId = 14, CategoryId = 6 },  
                new CategoryFilm { FilmId = 14, CategoryId = 9 },  
                new CategoryFilm { FilmId = 14, CategoryId = 10 }, 
                new CategoryFilm { FilmId = 15, CategoryId = 9 },  
                new CategoryFilm { FilmId = 15, CategoryId = 4 },  
                new CategoryFilm { FilmId = 15, CategoryId = 5 },  
                new CategoryFilm { FilmId = 16, CategoryId = 6 },  
                new CategoryFilm { FilmId = 16, CategoryId = 1 },  
                new CategoryFilm { FilmId = 16, CategoryId = 27 }, 
                new CategoryFilm { FilmId = 17, CategoryId = 27 }, 
                new CategoryFilm { FilmId = 17, CategoryId = 22 }, 
                new CategoryFilm { FilmId = 17, CategoryId = 4 },  
                new CategoryFilm { FilmId = 18, CategoryId = 1 },  
                new CategoryFilm { FilmId = 18, CategoryId = 6 },  
                new CategoryFilm { FilmId = 18, CategoryId = 5 },  
                new CategoryFilm { FilmId = 19, CategoryId = 4 },  
                new CategoryFilm { FilmId = 19, CategoryId = 23 },
                new CategoryFilm { FilmId = 19, CategoryId = 11 }, 
                new CategoryFilm { FilmId = 20, CategoryId = 6 },  
                new CategoryFilm { FilmId = 20, CategoryId = 5 },  
                new CategoryFilm { FilmId = 20, CategoryId = 10 },
                new CategoryFilm { FilmId = 21, CategoryId = 6 },  
                new CategoryFilm { FilmId = 21, CategoryId = 1 },  
                new CategoryFilm { FilmId = 21, CategoryId = 5 },  
                new CategoryFilm { FilmId = 21, CategoryId = 8 },
                new CategoryFilm { FilmId = 22, CategoryId = 9 },  
                new CategoryFilm { FilmId = 22, CategoryId = 8 },
                new CategoryFilm { FilmId = 22, CategoryId = 17 },
                new CategoryFilm { FilmId = 22, CategoryId = 10 }, 
                new CategoryFilm { FilmId = 23, CategoryId = 1 },  
                new CategoryFilm { FilmId = 23, CategoryId = 10 }, 
                new CategoryFilm { FilmId = 23, CategoryId = 9 },  
                new CategoryFilm { FilmId = 23, CategoryId = 8 },
                new CategoryFilm { FilmId = 24, CategoryId = 17 },
                new CategoryFilm { FilmId = 24, CategoryId = 8 },
                new CategoryFilm { FilmId = 24, CategoryId = 9 },  
                new CategoryFilm { FilmId = 24, CategoryId = 4 },  
                new CategoryFilm { FilmId = 25, CategoryId = 1 }, 
                new CategoryFilm { FilmId = 25, CategoryId = 10 }, 
                new CategoryFilm { FilmId = 25, CategoryId = 9 },
                new CategoryFilm { FilmId = 25, CategoryId = 8 },
                new CategoryFilm { FilmId = 26, CategoryId = 4 },
                new CategoryFilm { FilmId = 27, CategoryId = 9 },
                new CategoryFilm { FilmId = 27, CategoryId = 10 },
                new CategoryFilm { FilmId = 28, CategoryId = 22 },
                new CategoryFilm { FilmId = 29, CategoryId = 22 },
                new CategoryFilm { FilmId = 30, CategoryId = 4 },
                new CategoryFilm { FilmId = 31, CategoryId = 11 },
                new CategoryFilm { FilmId = 32, CategoryId = 9 },
                new CategoryFilm { FilmId = 32, CategoryId = 10 },
                new CategoryFilm { FilmId = 33, CategoryId = 22 },
                new CategoryFilm { FilmId = 34, CategoryId = 1 },
                new CategoryFilm { FilmId = 34, CategoryId = 22 },
                new CategoryFilm { FilmId = 35, CategoryId = 22 },
                new CategoryFilm { FilmId = 36, CategoryId = 9 },
                new CategoryFilm { FilmId = 36, CategoryId = 10 },
                new CategoryFilm { FilmId = 37, CategoryId = 22 },
                new CategoryFilm { FilmId = 38, CategoryId = 5 },
                new CategoryFilm { FilmId = 39, CategoryId = 22 },
                new CategoryFilm { FilmId = 40, CategoryId = 9 },
                new CategoryFilm { FilmId = 40, CategoryId = 10 },
                new CategoryFilm { FilmId = 41, CategoryId = 27 },
                new CategoryFilm { FilmId = 42, CategoryId = 4 },
                new CategoryFilm { FilmId = 43, CategoryId = 9 },
                new CategoryFilm { FilmId = 43, CategoryId = 10 },
                new CategoryFilm { FilmId = 44, CategoryId = 6 },
                new CategoryFilm { FilmId = 44, CategoryId = 27 },
                new CategoryFilm { FilmId = 45, CategoryId = 10 },
                new CategoryFilm { FilmId = 45, CategoryId = 1 },
                new CategoryFilm { FilmId = 45, CategoryId = 14 },
                new CategoryFilm { FilmId = 46, CategoryId = 2 },
                new CategoryFilm { FilmId = 47, CategoryId = 2 },
                new CategoryFilm { FilmId = 48, CategoryId = 2 },
                new CategoryFilm { FilmId = 49, CategoryId = 2 },
                new CategoryFilm { FilmId = 50, CategoryId = 8 },
                new CategoryFilm { FilmId = 51, CategoryId = 27 },
                new CategoryFilm { FilmId = 52, CategoryId = 9 },
                new CategoryFilm { FilmId = 53, CategoryId = 25 }
            );

        }
    }
}
