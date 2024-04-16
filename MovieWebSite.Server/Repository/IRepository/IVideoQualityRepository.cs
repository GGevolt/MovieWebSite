using MovieWebSite.Server.Models;

namespace MovieWebSite.Server.Repository.IRepository
{
	public interface IVideoQualityRepository : IRepository<VideoQuality>
	{
		void Update(VideoQuality videoQuality);
	}
}
