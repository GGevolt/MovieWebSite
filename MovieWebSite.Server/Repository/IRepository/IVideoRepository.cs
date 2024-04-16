using MovieWebSite.Server.Models;

namespace MovieWebSite.Server.Repository.IRepository
{
	public interface IVideoRepository : IRepository<Video>
	{
		void Update(Video video);
	}
}
