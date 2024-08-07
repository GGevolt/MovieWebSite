using Server.Model.Models;

namespace Server.DataAccess.Repository.IRepository
{
    public interface IEpisodeRepository : IRepository<Episode>
    {
        void Update(Episode episode);
    }
}
