using MovieWebSite.Server.Models;

namespace MovieWebSite.Server.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		void Update(Category category);
	}
}
