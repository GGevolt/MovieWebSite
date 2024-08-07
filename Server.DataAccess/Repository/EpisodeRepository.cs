using Server.DataAccess.Data;
using Server.DataAccess.Repository.IRepository;
using Server.Model.Models;

namespace Server.DataAccess.Repository
{
    internal class EpisodeRepository : Repository<Episode>, IEpisodeRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public EpisodeRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Episode episode)
        {
            _dbContext.Update(episode);
        }
    }
}
