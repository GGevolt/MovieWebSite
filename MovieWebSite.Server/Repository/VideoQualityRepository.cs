using MovieWebSite.Server.Data;
using MovieWebSite.Server.Models;
using MovieWebSite.Server.Repository.IRepository;

namespace MovieWebSite.Server.Repository
{
	internal class VideoQualityRepository : Repository<VideoQuality>, IVideoQualityRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public VideoQualityRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public void Update(VideoQuality videoQuality)
		{
			_dbContext.Update(videoQuality);
		}
	}
}
