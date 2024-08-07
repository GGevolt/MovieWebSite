using Server.DataAccess.Data;
using Server.DataAccess.Repository.IRepository;
using Server.Model.Models;

namespace Server.DataAccess.Repository
{
    internal class FilmRepository : Repository<Film>, IFilmRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public FilmRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
        public void Update(Film film)
        {
            _dbContext.Update(film);
        }
    }
}
