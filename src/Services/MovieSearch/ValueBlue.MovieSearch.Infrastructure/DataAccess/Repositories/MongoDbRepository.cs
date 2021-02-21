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
    public class MongoDbRepository<TEntity> : IRepository<TEntity>
    {
        private readonly IMongoDatabase _mongoDb;

        public MongoDbRepository(IMongoDatabase mongoDb)
        {
            _mongoDb = mongoDb;
        }

        protected IMongoCollection<TEntity> Collection =>
            _mongoDb.GetCollection<TEntity>(typeof(TEntity).Name.Pluralize());

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
            catch (Exception e)
            {
                throw; 
            }
        }

        public async Task<TEntity> FindOneAsync(
            Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken)
        {
            return await Collection
                .Find(filter)
                .FirstOrDefaultAsync(cancellationToken);
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