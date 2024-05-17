using MovieWebSite.Server.Data;
using MovieWebSite.Server.Models;
using MovieWebSite.Server.Repository.IRepository;

namespace MovieWebSite.Server.Repository
{
	internal class TrailerRepository : Repository<Trailer>, ITrailerRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public TrailerRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public void Update(Trailer trailer)
		{
			_dbContext.Update(trailer);
		}
	}
}
