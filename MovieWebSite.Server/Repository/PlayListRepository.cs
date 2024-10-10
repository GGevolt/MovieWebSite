using MovieWebSite.Server.Data;
using MovieWebSite.Server.Repository.IRepository;
using Server.Model.Models;

namespace MovieWebSite.Server.Repository
{
    internal class PlayListRepository : Repository<PlayList>, IPlayListRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public PlayListRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
        public void Update(PlayList playList)
        {
            _dbContext.Update(playList);
        }
    }
}
