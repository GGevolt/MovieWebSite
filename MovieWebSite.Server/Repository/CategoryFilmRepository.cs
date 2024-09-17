using MovieWebSite.Server.Data;
using MovieWebSite.Server.Repository.IRepository;
using Server.Model.Models;


namespace MovieWebSite.Server.Repository
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
