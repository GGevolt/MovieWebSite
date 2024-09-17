using Microsoft.EntityFrameworkCore;
using Server.Model.Models;

namespace MovieWebSite.Server.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<CategoryFilm> CategoryFilms { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option) : base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
                new Category { Id = 20, Name = "Western" }
                );

        }
    }
}
