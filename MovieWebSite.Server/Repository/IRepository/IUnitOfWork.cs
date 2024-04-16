namespace MovieWebSite.Server.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IVideoRepository VideoRepository { get; }
		IQualityRepository QualityRepository { get; }
		IVideoQualityRepository VideoQualityRepository { get; }
		IApplicationUserRepository ApplicationUserRepository { get; }
		ICategoryFilmRepository CategoryFilmRepository { get; }
		ICategoryRepository CategoryRepository { get; }
		IFilmRepository FilmRepository { get; }
		void Save();
	}
}
