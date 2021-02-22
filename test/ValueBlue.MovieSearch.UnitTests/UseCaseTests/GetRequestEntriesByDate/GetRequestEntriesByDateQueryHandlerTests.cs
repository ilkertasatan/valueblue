using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ValueBlue.MovieSearch.Application.Common.Model;
using ValueBlue.MovieSearch.Application.UseCases.GetRequestEntriesByDate;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.RequestEntries;
using Xunit;

namespace ValueBlue.MovieSearch.UnitTests.UseCaseTests.GetRequestEntriesByDate
{
    public class GetRequestEntriesByDateQueryHandlerTests
    {
        [Fact]
        public async Task Should_Return_Request_Entries_Given_Date_Period()
        {
            var expectedRequestEntries = new[]
            {
                new RequestEntry("search-token", "imdbId", 100, DateTime.Now.AddDays(-1), "127.0.0.1")
                {
                    Id = Guid.NewGuid().ToString()
                },
                new RequestEntry("search-token", "imdbId", 100, DateTime.Now, "127.0.0.1"){
                    Id = Guid.NewGuid().ToString()
                }
            };
            var notExpectedRequestEntry =
                new RequestEntry("search-token", "imdbId", 100, DateTime.Now.AddDays(-2), "127.0.0.1");
            var repositoryMock = new Mock<IRepository<RequestEntry>>();
            repositoryMock
                .Setup(x => x.FindManyAsync(
                    It.IsAny<Expression<Func<RequestEntry, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedRequestEntries);
            var sut = new GetRequestEntriesByDateQueryHandler(repositoryMock.Object);

            var actualResult = await sut.Handle(
                new GetRequestEntriesByDateQuery(DateTime.Now.AddDays(-1), DateTime.Now), CancellationToken.None);

            actualResult.Should()
                .BeOfType<GetRequestEntriesSuccessResult>()
                .Which.RequestEntries
                .Should().Contain(expectedRequestEntries).And.NotContain(notExpectedRequestEntry);
        }
    }
}