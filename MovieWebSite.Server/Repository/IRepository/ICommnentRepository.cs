using Server.Model.Models;

namespace MovieWebSite.Server.Repository.IRepository
{
    public interface ICommnentRepository : IRepository<Comment>
    {
        void Update(Comment comment);
    }
}
