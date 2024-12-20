﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieWebSite.Server.Data;

#nullable disable

namespace MovieWebSite.Server.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240919110200_seedData")]
    partial class seedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Server.Model.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Comedy"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Horror"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Drama"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Thriller"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Science Fiction"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Romance"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Animation"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Historical"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Shonen"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Sport"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Musical"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Idol"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Game"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Martial Arts"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Music"
                        },
                        new
                        {
                            Id = 19,
                            Name = "War"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Western"
                        },
                        new
                        {
                            Id = 21,
                            Name = "Mecha"
                        },
                        new
                        {
                            Id = 22,
                            Name = "Crime"
                        },
                        new
                        {
                            Id = 23,
                            Name = "Politics"
                        },
                        new
                        {
                            Id = 24,
                            Name = "Reality"
                        },
                        new
                        {
                            Id = 25,
                            Name = "Biography"
                        },
                        new
                        {
                            Id = 26,
                            Name = "Horror"
                        },
                        new
                        {
                            Id = 27,
                            Name = "Mystery"
                        },
                        new
                        {
                            Id = 28,
                            Name = "Adventure"
                        });
                });

            modelBuilder.Entity("Server.Model.Models.CategoryFilm", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "FilmId");

                    b.HasIndex("FilmId");

                    b.ToTable("CategoryFilms");

                    b.HasData(
                        new
                        {
                            CategoryId = 28,
                            FilmId = 1
                        },
                        new
                        {
                            CategoryId = 6,
                            FilmId = 1
                        },
                        new
                        {
                            CategoryId = 9,
                            FilmId = 1
                        },
                        new
                        {
                            CategoryId = 8,
                            FilmId = 1
                        },
                        new
                        {
                            CategoryId = 6,
                            FilmId = 2
                        },
                        new
                        {
                            CategoryId = 1,
                            FilmId = 2
                        },
                        new
                        {
                            CategoryId = 2,
                            FilmId = 2
                        },
                        new
                        {
                            CategoryId = 6,
                            FilmId = 3
                        },
                        new
                        {
                            CategoryId = 10,
                            FilmId = 3
                        },
                        new
                        {
                            CategoryId = 27,
                            FilmId = 3
                        },
                        new
                        {
                            CategoryId = 11,
                            FilmId = 4
                        },
                        new
                        {
                            CategoryId = 19,
                            FilmId = 4
                        },
                        new
                        {
                            CategoryId = 4,
                            FilmId = 4
                        },
                        new
                        {
                            CategoryId = 8,
                            FilmId = 5
                        },
                        new
                        {
                            CategoryId = 9,
                            FilmId = 5
                        },
                        new
                        {
                            CategoryId = 10,
                            FilmId = 5
                        },
                        new
                        {
                            CategoryId = 1,
                            FilmId = 6
                        },
                        new
                        {
                            CategoryId = 22,
                            FilmId = 6
                        },
                        new
                        {
                            CategoryId = 5,
                            FilmId = 6
                        },
                        new
                        {
                            CategoryId = 14,
                            FilmId = 7
                        },
                        new
                        {
                            CategoryId = 18,
                            FilmId = 7
                        },
                        new
                        {
                            CategoryId = 7,
                            FilmId = 7
                        },
                        new
                        {
                            CategoryId = 6,
                            FilmId = 8
                        },
                        new
                        {
                            CategoryId = 4,
                            FilmId = 8
                        },
                        new
                        {
                            CategoryId = 5,
                            FilmId = 8
                        },
                        new
                        {
                            CategoryId = 9,
                            FilmId = 9
                        },
                        new
                        {
                            CategoryId = 10,
                            FilmId = 9
                        },
                        new
                        {
                            CategoryId = 6,
                            FilmId = 9
                        },
                        new
                        {
                            CategoryId = 4,
                            FilmId = 10
                        },
                        new
                        {
                            CategoryId = 22,
                            FilmId = 10
                        },
                        new
                        {
                            CategoryId = 27,
                            FilmId = 10
                        },
                        new
                        {
                            CategoryId = 9,
                            FilmId = 11
                        },
                        new
                        {
                            CategoryId = 4,
                            FilmId = 11
                        },
                        new
                        {
                            CategoryId = 10,
                            FilmId = 11
                        },
                        new
                        {
                            CategoryId = 1,
                            FilmId = 12
                        },
                        new
                        {
                            CategoryId = 6,
                            FilmId = 12
                        },
                        new
                        {
                            CategoryId = 5,
                            FilmId = 12
                        },
                        new
                        {
                            CategoryId = 22,
                            FilmId = 13
                        },
                        new
                        {
                            CategoryId = 4,
                            FilmId = 13
                        },
                        new
                        {
                            CategoryId = 6,
                            FilmId = 14
                        },
                        new
                        {
                            CategoryId = 9,
                            FilmId = 14
                        },
                        new
                        {
                            CategoryId = 10,
                            FilmId = 14
                        },
                        new
                        {
                            CategoryId = 9,
                            FilmId = 15
                        },
                        new
                        {
                            CategoryId = 4,
                            FilmId = 15
                        },
                        new
                        {
                            CategoryId = 5,
                            FilmId = 15
                        },
                        new
                        {
                            CategoryId = 6,
                            FilmId = 16
                        },
                        new
                        {
                            CategoryId = 1,
                            FilmId = 16
                        },
                        new
                        {
                            CategoryId = 27,
                            FilmId = 16
                        },
                        new
                        {
                            CategoryId = 27,
                            FilmId = 17
                        },
                        new
                        {
                            CategoryId = 22,
                            FilmId = 17
                        },
                        new
                        {
                            CategoryId = 4,
                            FilmId = 17
                        },
                        new
                        {
                            CategoryId = 1,
                            FilmId = 18
                        },
                        new
                        {
                            CategoryId = 6,
                            FilmId = 18
                        },
                        new
                        {
                            CategoryId = 5,
                            FilmId = 18
                        },
                        new
                        {
                            CategoryId = 4,
                            FilmId = 19
                        },
                        new
                        {
                            CategoryId = 23,
                            FilmId = 19
                        },
                        new
                        {
                            CategoryId = 11,
                            FilmId = 19
                        },
                        new
                        {
                            CategoryId = 6,
                            FilmId = 20
                        },
                        new
                        {
                            CategoryId = 5,
                            FilmId = 20
                        },
                        new
                        {
                            CategoryId = 10,
                            FilmId = 20
                        },
                        new
                        {
                            CategoryId = 6,
                            FilmId = 21
                        },
                        new
                        {
                            CategoryId = 1,
                            FilmId = 21
                        },
                        new
                        {
                            CategoryId = 5,
                            FilmId = 21
                        },
                        new
                        {
                            CategoryId = 8,
                            FilmId = 21
                        },
                        new
                        {
                            CategoryId = 9,
                            FilmId = 22
                        },
                        new
                        {
                            CategoryId = 8,
                            FilmId = 22
                        },
                        new
                        {
                            CategoryId = 17,
                            FilmId = 22
                        },
                        new
                        {
                            CategoryId = 10,
                            FilmId = 22
                        },
                        new
                        {
                            CategoryId = 1,
                            FilmId = 23
                        },
                        new
                        {
                            CategoryId = 10,
                            FilmId = 23
                        },
                        new
                        {
                            CategoryId = 9,
                            FilmId = 23
                        },
                        new
                        {
                            CategoryId = 8,
                            FilmId = 23
                        },
                        new
                        {
                            CategoryId = 17,
                            FilmId = 24
                        },
                        new
                        {
                            CategoryId = 8,
                            FilmId = 24
                        },
                        new
                        {
                            CategoryId = 9,
                            FilmId = 24
                        },
                        new
                        {
                            CategoryId = 4,
                            FilmId = 24
                        },
                        new
                        {
                            CategoryId = 1,
                            FilmId = 25
                        },
                        new
                        {
                            CategoryId = 10,
                            FilmId = 25
                        },
                        new
                        {
                            CategoryId = 9,
                            FilmId = 25
                        },
                        new
                        {
                            CategoryId = 8,
                            FilmId = 25
                        });
                });

            modelBuilder.Entity("Server.Model.Models.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EpisodeNumber")
                        .HasColumnType("int");

                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<string>("VidName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("Server.Model.Models.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilmImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Synopsis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Films");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Director = "Masayuki Kojima",
                            FilmImg = "Made_in_Abyss_volume_1_cover.jpg",
                            Synopsis = "The story takes place in the fictional town of Orth, where humans are drawn to a mysterious pit known as the \"Abyss\". The series follows Riko, a young orphan, who becomes obsessed with exploring the Abyss, despite the danger and unknown terrors that lurk within. Alongside her companion, Reg, they delve into the depths of the Abyss, uncovering secrets and facing challenges that threaten their lives. The series explores themes of discovery, friendship, and the human condition.",
                            Title = "Made in Abyss",
                            Type = "TV-Series"
                        },
                        new
                        {
                            Id = 2,
                            Director = "Shawn Levy",
                            FilmImg = "2dc59b7ae3022ab3ea57c1d6ad94399a.jpg",
                            Synopsis = "A listless Wade Wilson toils away in civilian life with his days as the morally flexible mercenary, Deadpool, behind him. But when his homeworld faces an existential threat, Wade must reluctantly suit-up again with an even more reluctant Wolverine.",
                            Title = "Deadpool & Wolverine",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = 3,
                            Director = "Christopher Nolan",
                            FilmImg = "MV5BMjExMjkwNTQ0Nl5BMl5BanBnXkFtZTcwNTY0OTk1Mw@@._V1_.jpg",
                            Synopsis = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.",
                            Title = "Inception",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = 4,
                            Director = "Ridley Scott",
                            FilmImg = "Gladiator_(2000_film_poster).png",
                            Synopsis = "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.",
                            Title = "Gladiator",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = 5,
                            Director = "Hayao Miyazaki",
                            FilmImg = "MV5BMjlmZmI5MDctNDE2YS00YWE0LWE5ZWItZDBhYWQ0NTcxNWRhXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_.jpg",
                            Synopsis = "During her family's move to the suburbs, a sullen 10-year-old girl wanders into a world ruled by gods, witches, and spirits, and where humans are changed into beasts.",
                            Title = "Spirited Away",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = 6,
                            Director = "Christopher Nolan",
                            FilmImg = "MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_.jpg",
                            Synopsis = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                            Title = "The Dark Knight",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = 7,
                            Director = "Damien Chazelle",
                            FilmImg = "MV5BMzUzNDM2NzM2MV5BMl5BanBnXkFtZTgwNTM3NTg4OTE@._V1_.jpg",
                            Synopsis = "While navigating their careers in Los Angeles, a pianist and an actress fall in love while attempting to reconcile their aspirations for the future.",
                            Title = "La La Land",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = 8,
                            Director = "The Duffer Brothers",
                            FilmImg = "image5.jpg",
                            Synopsis = "When a young boy disappears, his mother, a police chief, and his friends must confront terrifying supernatural forces in order to get him back.",
                            Title = "Stranger Things",
                            Type = "TV-Series"
                        },
                        new
                        {
                            Id = 9,
                            Director = "Jon Favreau",
                            FilmImg = "var-www-share-shop-live-src-media-import-iadx4-095-the-mandalorian-gunslingers-web.jpg",
                            Synopsis = "The travels of a lone bounty hunter in the outer reaches of the galaxy, far from the authority of the New Republic.",
                            Title = "The Mandalorian",
                            Type = "TV-Series"
                        },
                        new
                        {
                            Id = 10,
                            Director = "Bong Joon-ho",
                            FilmImg = "MV5BYWZjMjk3ZTItODQ2ZC00NTY5LWE0ZDYtZTI3MjcwN2Q5NTVkXkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_.jpg",
                            Synopsis = "Greed and class discrimination threaten the newly formed symbiotic relationship between the wealthy Park family and the destitute Kim clan.",
                            Title = "Parasite",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = 11,
                            Director = "Lauren Schmidt Hissrich",
                            FilmImg = "p17580215_b_v13_ab.jpg",
                            Synopsis = "Geralt of Rivia, a mutated monster hunter for hire, journeys toward his destiny in a turbulent world where people often prove more wicked than beasts.",
                            Title = "The Witcher",
                            Type = "TV-Series"
                        },
                        new
                        {
                            Id = 12,
                            Director = "Anthony and Joe Russo",
                            FilmImg = "MV5BMTc5MDE2ODcwNV5BMl5BanBnXkFtZTgwMzI2NzQ2NzM@._V1_.jpg",
                            Synopsis = "After the devastating events of Avengers: Infinity War, the universe is in ruins. With the help of remaining allies, the Avengers assemble once more to reverse Thanos' actions and restore balance to the universe.",
                            Title = "Avengers: Endgame",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = 13,
                            Director = "Vince Gilligan",
                            FilmImg = "MV5BYmQ4YWMxYjUtNjZmYi00MDQ1LWFjMjMtNjA5ZDdiYjdiODU5XkEyXkFqcGdeQXVyMTMzNDExODE5._V1_.jpg",
                            Synopsis = "A high school chemistry teacher turned methamphetamine manufacturer partners with a former student in a gradual descent into the criminal underworld.",
                            Title = "Breaking Bad",
                            Type = "TV-Series"
                        },
                        new
                        {
                            Id = 14,
                            Director = "James Cameron",
                            FilmImg = "MV5BZDA0OGQxNTItMDZkMC00N2UyLTg3MzMtYTJmNjg3Nzk5MzRiXkEyXkFqcGdeQXVyMjUzOTY1NTc@._V1_FMjpg_UX1000_.jpg",
                            Synopsis = "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.",
                            Title = "Avatar",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = 15,
                            Director = "David Benioff, D.B. Weiss",
                            FilmImg = "MV5BN2IzYzBiOTQtNGZmMi00NDI5LTgxMzMtN2EzZjA1NjhlOGMxXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_.jpg",
                            Synopsis = "Nine noble families wage war against each other in order to gain control over the mythical land of Westeros.",
                            Title = "Game of Thrones",
                            Type = "TV-Series"
                        },
                        new
                        {
                            Id = 16,
                            Director = "The Wachowskis",
                            FilmImg = "613ypTLZHsL._AC_UF894,1000_QL80_.jpg",
                            Synopsis = "A computer hacker learns about the true nature of his reality and his role in the war against its controllers.",
                            Title = "The Matrix",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = 17,
                            Director = "Mark Gatiss, Steven Moffat",
                            FilmImg = "MV5BNTQzNGZjNDEtOTMwYi00MzFjLWE2ZTYtYzYxYzMwMjZkZDc5XkEyXkFqcGc@._V1_.jpg",
                            Synopsis = "A modern update finds the famous sleuth and his doctor partner solving crime in 21st century London.",
                            Title = "Sherlock",
                            Type = "TV-Series"
                        },
                        new
                        {
                            Id = 18,
                            Director = "George Miller",
                            FilmImg = "p10854488_p_v8_ac.jpg",
                            Synopsis = "In a post-apocalyptic wasteland, Max teams up with a mysterious woman, Furiosa, to try and survive.",
                            Title = "Mad Max: Fury Road",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = 19,
                            Director = "Peter Morgan",
                            FilmImg = "71f97nf-u9L._AC_UF1000,1000_QL80_.jpg",
                            Synopsis = "Follows the political rivalries and romance of Queen Elizabeth II's reign and the events that shaped the second half of the 20th century.",
                            Title = "The Crown",
                            Type = "TV-Series"
                        },
                        new
                        {
                            Id = 20,
                            Director = "Christopher Nolan",
                            FilmImg = "MV5BZjdkOTU3MDktN2IxOS00OGEyLWFmMjktY2FiMmZkNWIyODZiXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_.jpg",
                            Synopsis = "A team of explorers travels through a wormhole in space in an attempt to ensure humanity's survival.",
                            Title = "Interstellar",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = 21,
                            Director = "Tetsurō Araki",
                            FilmImg = "p10701949_b_v8_ah.jpg",
                            Synopsis = "After his hometown is destroyed and his mother is killed, young Eren Jaeger vows to cleanse the earth of the giant humanoid Titans that have brought humanity to the brink of extinction.",
                            Title = "Attack on Titan",
                            Type = "TV-Series"
                        },
                        new
                        {
                            Id = 22,
                            Director = "Masayuki Kojima",
                            FilmImg = "unnamed.jpg",
                            Synopsis = "Riko, Reg, and their new friend, Nanachi, continue their journey down the Abyss and arrive at the 5th layer. But in order for them to continue to the 6th layer, they must encounter the haunting figure of Nanachi's past: Bondrewd the Novel.",
                            Title = "Made In Abyss: Dawn of The Deep Soul",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = 23,
                            Director = "Hayato Date",
                            FilmImg = "MV5BZmQ5NGFiNWEtMmMyMC00MDdiLTg4YjktOGY5Yzc2MDUxMTE1XkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_FMjpg_UX1000_.jpg",
                            Synopsis = "Follows Naruto Uzumaki, a young ninja with dreams of becoming the strongest ninja and leader of his village.",
                            Title = "Naruto",
                            Type = "TV-Series"
                        },
                        new
                        {
                            Id = 24,
                            Director = "Hayao Miyazaki",
                            FilmImg = "MV5BYWM3MDE3YjEtMzIzZC00ODE5LTgxNTItNmUyMTBkM2M2NmNiXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                            Synopsis = "When two girls move to the countryside to be near their ailing mother, they have adventures with the wondrous forest spirits who live nearby.",
                            Title = "My Neighbor Totoro",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = 25,
                            Director = "Haruo Sotozaki",
                            FilmImg = "MV5BYTIxNjk3YjItYmYzMC00ZTdmLTk0NGUtZmNlZTA0NWFkZDMwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_.jpg",
                            Synopsis = "A young boy becomes a demon slayer to avenge his family and cure his sister.",
                            Title = "Demon Slayer: Kimetsu no Yaiba",
                            Type = "TV-Series"
                        });
                });

            modelBuilder.Entity("Server.Model.Models.CategoryFilm", b =>
                {
                    b.HasOne("Server.Model.Models.Category", "Category")
                        .WithMany("CategoryFilms")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Model.Models.Film", "Film")
                        .WithMany("CategoryFilms")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Film");
                });

            modelBuilder.Entity("Server.Model.Models.Episode", b =>
                {
                    b.HasOne("Server.Model.Models.Film", "Film")
                        .WithMany("Episodes")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");
                });

            modelBuilder.Entity("Server.Model.Models.Category", b =>
                {
                    b.Navigation("CategoryFilms");
                });

            modelBuilder.Entity("Server.Model.Models.Film", b =>
                {
                    b.Navigation("CategoryFilms");

                    b.Navigation("Episodes");
                });
#pragma warning restore 612, 618
        }
    }
}
