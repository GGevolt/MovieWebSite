using MovieWebSite.Server.Models;

namespace MovieWebSite.Server.Repository.IRepository
{
    public interface IUserFilmRepository : IRepository<UserFilm>
    {
        void Update(UserFilm userFilm);
    }
}
