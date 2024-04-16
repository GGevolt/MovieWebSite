using MovieWebSite.Server.Models;

namespace MovieWebSite.Server.Repository.IRepository
{
	public interface IQualityRepository : IRepository<Quality>
	{
		void Update(Quality quality);
	}
}
