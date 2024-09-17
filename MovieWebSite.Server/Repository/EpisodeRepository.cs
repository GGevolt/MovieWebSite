using MovieWebSite.Server.Data;
using Server.Model.Models;
using MovieWebSite.Server.Repository.IRepository;

namespace MovieWebSite.Server.Repository
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
