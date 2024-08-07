using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Server.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperty = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperty = null, bool tracker = false);
        void Add(T entity);
        void Remove(T entity);
    }
}
