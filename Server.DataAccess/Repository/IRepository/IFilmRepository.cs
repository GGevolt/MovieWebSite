using Server.Model.Models;

namespace Server.DataAccess.Repository.IRepository
{
    public interface IFilmRepository : IRepository<Film>
    {
        void Update(Film film);
    }
}
