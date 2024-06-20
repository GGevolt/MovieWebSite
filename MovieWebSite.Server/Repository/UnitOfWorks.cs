using MovieWebSite.Server.Data;
using MovieWebSite.Server.Repository.IRepository;

namespace MovieWebSite.Server.Repository
{
	public class UnitOfWorks : IUnitOfWork
	{
		private readonly ApplicationDBContext _dbContext;
		public IEpisodeRepository EpisodeRepository { get; private set; }
		public IVideoRepository VideoRepository { get; private set; }
		public IApplicationUserRepository ApplicationUserRepository { get; private set; }
		public IFilmRepository FilmRepository { get; private set; }
		public ICategoryRepository CategoryRepository { get; private set; }
		public ICategoryFilmRepository CategoryFilmRepository { get; private set; }

		public UnitOfWorks(ApplicationDBContext dBContext)
		{
			_dbContext = dBContext;
			ApplicationUserRepository = new ApplicationUserRepository(dBContext);
			EpisodeRepository = new EpisodeRepository(dBContext);
			VideoRepository = new VideoRepository(dBContext);
			FilmRepository = new FilmRepository(dBContext);
			CategoryRepository = new CategoryRepository(dBContext);
			CategoryFilmRepository = new CategoryFilmRepository(dBContext);
		}

		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}
