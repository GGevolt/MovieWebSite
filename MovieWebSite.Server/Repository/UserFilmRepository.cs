using MovieWebSite.Server.Data;
using MovieWebSite.Server.Repository.IRepository;
using Server.Model.Models;

namespace MovieWebSite.Server.Repository
{
    internal class UserFilmRepository : Repository<UserFilm>, IUserFilmRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserFilmRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
        public void Update(UserFilm userFilm)
        {
            _dbContext.Update(userFilm);
        }
    }
}
