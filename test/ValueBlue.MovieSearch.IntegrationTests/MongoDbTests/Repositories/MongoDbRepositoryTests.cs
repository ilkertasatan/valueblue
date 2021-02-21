using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Bson;
using ValueBlue.MovieSearch.Infrastructure.DataAccess.Repositories;
using Xunit;

namespace ValueBlue.MovieSearch.IntegrationTests.MongoDbTests.Repositories
{
    public class MongoDbRepositoryTests :
        IClassFixture<DatabaseFixture>
    {
        private readonly CancellationToken _cancellationToken;
        private readonly MongoDbRepository<FakeEntity> _sut;

        public MongoDbRepositoryTests(DatabaseFixture fixture)
        {
            _sut = new MongoDbRepository<FakeEntity>(fixture.Database);

            var cancellation = new CancellationTokenSource();
            cancellation.CancelAfter(TimeSpan.FromSeconds(3));

            _cancellationToken = cancellation.Token;
        }

        [Fact]
        public async Task Insert_One()
        {
            var expectedEntity = new FakeEntity(ObjectId.GenerateNewId(), "field1", "field2");

            await _sut.InsertOneAsync(expectedEntity, _cancellationToken);

            var actualEntity = await _sut.FindOneAsync(x => x.Id == expectedEntity.Id, _cancellationToken);
            actualEntity.Should()
                .BeEquivalentTo(expectedEntity);
        }
        
        [Fact]
        public async Task Find_One()
        {
            var expectedId = ObjectId.GenerateNewId();
            var expectedEntity = new FakeEntity(expectedId, "field1", "field2");
            await _sut.InsertOneAsync(expectedEntity, _cancellationToken);

            var actualEntities = await _sut.FindOneAsync(x => x.Id == expectedId, _cancellationToken);

            actualEntities.Should()
                .BeOfType<FakeEntity>()
                .Which.Should()
                .BeEquivalentTo(expectedEntity);
        }

        [Fact]
        public async Task Find_Many()
        {
            var expectedField = Guid.NewGuid().ToString();
            var expectedEntity1 = new FakeEntity(ObjectId.GenerateNewId(), expectedField, "field2");
            var expectedEntity2 = new FakeEntity(ObjectId.GenerateNewId(), expectedField, "field2");
            await _sut.InsertOneAsync(expectedEntity1, _cancellationToken);
            await _sut.InsertOneAsync(expectedEntity2, _cancellationToken);

            var actualEntities = await _sut.FindManyAsync(x => x.Field1 == expectedField, _cancellationToken);

            actualEntities.Should()
                .NotBeEmpty()
                .And
                .HaveCount(2)
                .And
                .Contain(expectedEntity1)
                .And
                .Contain(expectedEntity2);
        }
    }
}