using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieWebSite.Server.Models;
using System.Reflection.Emit;

namespace MovieWebSite.Server.Data
{
	public class ApplicationDBContext : DbContext
	{
		public DbSet<Category> Categories { get; set; }
		public DbSet<Film> Films { get; set; }
		public DbSet<CategoryFilm> CategoryFilms { get; set; }
		public DbSet<Trailer> Trailers { get; set; }
		public DbSet<Episode> Episodes {  get; set; }
		public DbSet<Quality> Qualities { get; set; }
		public DbSet<VideoQuality> VideoQualities { get; set; }
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option) : base(option)
		{


		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<VideoQuality>()
				.HasKey(vq => new { vq.TrailerId, vq.EpisodeId, vq.QualityId });

			modelBuilder.Entity<VideoQuality>()
				.HasOne(vq => vq.Trailer)
				.WithMany(t => t.VideoQualities)
				.HasForeignKey(vq => vq.TrailerId);

            modelBuilder.Entity<VideoQuality>()
                .HasOne(vq => vq.Episode)
                .WithMany(e => e.VideoQualities)
                .HasForeignKey(vq => vq.EpisodeId);

            modelBuilder.Entity<VideoQuality>()
				.HasOne(vq => vq.Quality)
				.WithMany(q => q.VideoQualities)
				.HasForeignKey(vq => vq.QualityId);

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
				new Category { Id = 1, Name = "Action"},
				new Category { Id = 2, Name = "Comedy"}
				);

        }
	}
}
