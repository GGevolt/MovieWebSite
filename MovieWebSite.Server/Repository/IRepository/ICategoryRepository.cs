using Server.Model.Models;

namespace MovieWebSite.Server.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
