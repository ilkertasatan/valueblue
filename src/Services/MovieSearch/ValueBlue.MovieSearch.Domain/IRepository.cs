using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ValueBlue.MovieSearch.Domain
{
    public interface IRepository<T>
    {
        Task InsertOneAsync(T entity, CancellationToken cancellationToken);
        Task<T> FindOneAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);
        Task DeleteOneAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);
        Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);
    }
}