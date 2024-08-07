using Server.Model.Models;

namespace Server.DataAccess.Repository.IRepository
{
    public interface ICategoryFilmRepository : IRepository<CategoryFilm>
    {
        void Update(CategoryFilm categoryFilm);
    }
}
