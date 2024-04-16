using MovieWebSite.Server.Data;
using MovieWebSite.Server.Models;
using MovieWebSite.Server.Repository.IRepository;

namespace MovieWebSite.Server.Repository
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
