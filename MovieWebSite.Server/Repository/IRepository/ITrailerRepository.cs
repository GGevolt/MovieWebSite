using MovieWebSite.Server.Models;

namespace MovieWebSite.Server.Repository.IRepository
{
	public interface ITrailerRepository : IRepository<Trailer>
	{
		void Update(Trailer trailer);
	}
}
