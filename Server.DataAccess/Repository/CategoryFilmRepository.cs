using Server.DataAccess.Data;
using Server.DataAccess.Repository.IRepository;
using Server.Model.Models;


namespace Server.DataAccess.Repository
{
    internal class CategoryFilmRepository : Repository<CategoryFilm>, ICategoryFilmRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryFilmRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
        public void Update(CategoryFilm categoryFilm)
        {
            _dbContext.Update(categoryFilm);
        }
    }
}
