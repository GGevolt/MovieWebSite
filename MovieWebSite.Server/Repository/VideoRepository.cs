using MovieWebSite.Server.Data;
using MovieWebSite.Server.Models;
using MovieWebSite.Server.Repository.IRepository;

namespace MovieWebSite.Server.Repository
{
	internal class VideoRepository : Repository<Video>, IVideoRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public VideoRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public void Update(Video video)
		{
			_dbContext.Update(video);
		}
	}
}
