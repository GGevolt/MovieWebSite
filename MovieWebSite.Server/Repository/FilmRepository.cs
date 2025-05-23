﻿using MovieWebSite.Server.Data;
using MovieWebSite.Server.Repository.IRepository;
using MovieWebSite.Server.Models;

namespace MovieWebSite.Server.Repository
{
    internal class FilmRepository : Repository<Film>, IFilmRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public FilmRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
        public void Update(Film film)
        {
            _dbContext.Update(film);
        }
    }
}
