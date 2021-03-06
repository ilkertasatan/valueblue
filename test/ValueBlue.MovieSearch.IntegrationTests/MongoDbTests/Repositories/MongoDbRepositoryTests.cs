﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using ValueBlue.MovieSearch.Domain.Movies;
using ValueBlue.MovieSearch.Domain.RequestEntries;
using ValueBlue.MovieSearch.Infrastructure.DataAccess.Repositories;
using Xunit;

namespace ValueBlue.MovieSearch.IntegrationTests.MongoDbTests.Repositories
{
    public class MongoDbRepositoryTests :
        IClassFixture<DatabaseFixture>
    {
        private readonly CancellationToken _cancellationToken;
        private readonly Repository<FakeEntity> _sut;

        public MongoDbRepositoryTests(DatabaseFixture fixture)
        {
            _sut = new Repository<FakeEntity>(fixture.Database);

            var cancellation = new CancellationTokenSource();
            cancellation.CancelAfter(TimeSpan.FromSeconds(3));

            _cancellationToken = cancellation.Token;
        }

        [Fact]
        public async Task Should_Insert_One()
        {
            var expectedId = Guid.NewGuid();
            var expectedEntity = new FakeEntity(expectedId, "field1", "field2");

            await _sut.InsertOneAsync(expectedEntity, _cancellationToken);

            var actualEntity = await _sut.FindOneAsync(x => x.Id == expectedId, _cancellationToken);
            actualEntity.Should()
                .BeEquivalentTo(expectedEntity);
        }
        
        [Fact]
        public async Task Should_Find_One()
        {
            var expectedId = Guid.NewGuid();
            var expectedEntity = new FakeEntity(expectedId, "field1", "field2");
            await _sut.InsertOneAsync(expectedEntity, _cancellationToken);

            var actualEntities = await _sut.FindOneAsync(x => x.Id == expectedId, _cancellationToken);

            actualEntities.Should()
                .BeOfType<FakeEntity>()
                .Which.Should()
                .BeEquivalentTo(expectedEntity);
        }

        [Fact]
        public async Task Should_Find_Many()
        {
            var expectedField = Guid.NewGuid().ToString();
            var expectedEntity1 = new FakeEntity(Guid.NewGuid(), expectedField, "field2");
            var expectedEntity2 = new FakeEntity(Guid.NewGuid(), expectedField, "field2");
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
        
        [Fact]
        public async Task Should_Delete_One()
        {
            var expectedId = Guid.NewGuid();
            var expectedEntity = new FakeEntity(expectedId, "field1", "field2");
            await _sut.InsertOneAsync(expectedEntity, _cancellationToken);

            await _sut.DeleteOneAsync(x => x.Id == expectedId, _cancellationToken);

            var actualEntity = await _sut.FindOneAsync(x => x.Id == expectedId, _cancellationToken);
            actualEntity.Should().Be(FakeEntity.Empty);
        }
    }
}