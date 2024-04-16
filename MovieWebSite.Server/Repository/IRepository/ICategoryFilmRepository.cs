using MovieWebSite.Server.Models;

namespace MovieWebSite.Server.Repository.IRepository
{
	public interface ICategoryFilmRepository : IRepository<CategoryFilm>
	{
		void Update(CategoryFilm categoryFilm);
	}
}
