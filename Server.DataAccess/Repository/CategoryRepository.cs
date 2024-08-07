using Server.DataAccess.Data;
using Server.DataAccess.Repository.IRepository;
using Server.Model.Models;

namespace Server.DataAccess.Repository
{
    internal class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public void Update(Category category)
        {
            _dbContext.Update(category);
        }
    }
}
