using MovieWebSite.Server.Data;
using MovieWebSite.Server.Repository.IRepository;

namespace MovieWebSite.Server.Repository
{
	public class UnitOfWorks : IUnitOfWork
	{
		private readonly ApplicationDBContext _dbContext;
		public ITrailerRepository TrailerRepository { get; private set; }
		public IEpisodeRepository EpisodeRepository { get; private set; }
		public IQualityRepository QualityRepository { get; private set; }
		public IVideoQualityRepository VideoQualityRepository { get; private set; }
		public IApplicationUserRepository ApplicationUserRepository { get; private set; }
		public IFilmRepository FilmRepository { get; private set; }
		public ICategoryRepository CategoryRepository { get; private set; }
		public ICategoryFilmRepository CategoryFilmRepository { get; private set; }

		public UnitOfWorks(ApplicationDBContext dBContext)
		{
			_dbContext = dBContext;
			ApplicationUserRepository = new ApplicationUserRepository(dBContext);
            TrailerRepository = new TrailerRepository(dBContext);
			EpisodeRepository = new EpisodeRepository(dBContext);
			QualityRepository = new QualityRepository(dBContext);
			VideoQualityRepository = new VideoQualityRepository(dBContext);
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
