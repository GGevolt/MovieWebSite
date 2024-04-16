using MovieWebSite.Server.Models;

namespace MovieWebSite.Server.Repository.IRepository
{
	public interface IFilmRepository : IRepository<Film>
	{
		void Update(Film film);
	}
}
