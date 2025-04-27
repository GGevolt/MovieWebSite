using MovieWebSite.Server.Data;
using MovieWebSite.Server.Models;
using MovieWebSite.Server.Repository.IRepository;

namespace MovieWebSite.Server.Repository
{
    internal class CommentRepository : Repository<Comment>, ICommnentRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CommentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(Comment comment)
        {
            _dbContext.Update(comment);
        }
    }
}
