namespace MovieWebSite.Server.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IEpisodeRepository EpisodeRepository { get; }
        ICategoryFilmRepository CategoryFilmRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IFilmRepository FilmRepository { get; }
        void Save();
    }
}
