using Server.Model.Models;

namespace MovieWebSite.Server.Repository.IRepository
{
    public interface IPlayListRepository : IRepository<PlayList>
    {
        void Update(PlayList playList);
    }
}
