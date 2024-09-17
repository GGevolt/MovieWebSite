using Server.Model.Models;

namespace MovieWebSite.Server.Repository.IRepository
{
    public interface IEpisodeRepository : IRepository<Episode>
    {
        void Update(Episode episode);
    }
}
