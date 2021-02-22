using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using MongoDB.Driver;
using ValueBlue.MovieSearch.Domain;

namespace ValueBlue.MovieSearch.Infrastructure.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : new()
    {
        public Repository(IMongoDatabase mongoDb)
        {
            Collection = mongoDb.GetCollection<TEntity>(typeof(TEntity).Name.Pluralize());
        }

        protected IMongoCollection<TEntity> Collection { get; }
        
        public async Task InsertOneAsync(
            TEntity entity,
            CancellationToken cancellationToken)
        {
            try
            {
                await Collection
                    .InsertOneAsync(entity, cancellationToken: cancellationToken);
            }
            catch (MongoWriteException mongoWriteException)
            {
                var writeErrorCode = mongoWriteException.WriteError.Code;
                if (writeErrorCode == 11000)
                    throw new Exception(mongoWriteException.Message);

                throw;
            }
        }

        public async Task<TEntity> FindOneAsync(
            Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken)
        {
            var entity = await Collection
                .Find(filter)
                .FirstOrDefaultAsync(cancellationToken);
            
            return entity ?? new TEntity();
        }

        public async Task DeleteOneAsync(
            Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken)
        {
            await Collection.DeleteOneAsync(filter, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> FindManyAsync(
            Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken)
        {
            return await Collection
                .Find(filter)
                .ToListAsync(cancellationToken);
        }
    }
}