using MovieWebSite.Server.Data;
using MovieWebSite.Server.Models;
using MovieWebSite.Server.Repository.IRepository;

namespace MovieWebSite.Server.Repository
{
	internal class QualityRepository : Repository<Quality>, IQualityRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public QualityRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public void Update(Quality quality)
		{
			_dbContext.Update(quality);
		}
	}
}
