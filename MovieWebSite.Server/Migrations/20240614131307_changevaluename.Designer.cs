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
    [Migration("20240614131307_changevaluename")]
    partial class changevaluename
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MovieWebSite.Server.Models.Category", b =>
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
                        });
                });

            modelBuilder.Entity("MovieWebSite.Server.Models.CategoryFilm", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "FilmId");

                    b.HasIndex("FilmId");

                    b.ToTable("CategoryFilms");
                });

            modelBuilder.Entity("MovieWebSite.Server.Models.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EpisodeNumber")
                        .HasColumnType("int");

                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("MovieWebSite.Server.Models.Film", b =>
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
                });

            modelBuilder.Entity("MovieWebSite.Server.Models.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EpisodeId")
                        .HasColumnType("int");

                    b.Property<string>("VidName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EpisodeId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("MovieWebSite.Server.Models.CategoryFilm", b =>
                {
                    b.HasOne("MovieWebSite.Server.Models.Category", "Category")
                        .WithMany("CategoryFilms")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieWebSite.Server.Models.Film", "Film")
                        .WithMany("CategoryFilms")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Film");
                });

            modelBuilder.Entity("MovieWebSite.Server.Models.Episode", b =>
                {
                    b.HasOne("MovieWebSite.Server.Models.Film", "Film")
                        .WithMany("Episodes")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");
                });

            modelBuilder.Entity("MovieWebSite.Server.Models.Video", b =>
                {
                    b.HasOne("MovieWebSite.Server.Models.Episode", "Episode")
                        .WithMany("Videos")
                        .HasForeignKey("EpisodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Episode");
                });

            modelBuilder.Entity("MovieWebSite.Server.Models.Category", b =>
                {
                    b.Navigation("CategoryFilms");
                });

            modelBuilder.Entity("MovieWebSite.Server.Models.Episode", b =>
                {
                    b.Navigation("Videos");
                });

            modelBuilder.Entity("MovieWebSite.Server.Models.Film", b =>
                {
                    b.Navigation("CategoryFilms");

                    b.Navigation("Episodes");
                });
#pragma warning restore 612, 618
        }
    }
}
