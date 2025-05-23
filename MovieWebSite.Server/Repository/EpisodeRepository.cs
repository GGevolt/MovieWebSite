﻿using MovieWebSite.Server.Data;
using MovieWebSite.Server.Repository.IRepository;
using MovieWebSite.Server.Models;

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
