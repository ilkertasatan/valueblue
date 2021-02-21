using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ValueBlue.MovieSearch.Application.UseCases.GetAllRequestEntries;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.RequestEntries;
using Xunit;

namespace ValueBlue.MovieSearch.UnitTests.UseCaseTests.GetAllRequestEntries
{
    public class AllRequestEntriesQueryHandlerTests
    {
        [Fact]
        public async Task Should_Return_Request_Entries()
        {
            var expectedRequestEntries = new[]
            {
                new RequestEntry("search-token", "imdbId", 100, DateTime.Now, "127.0.0.1")
            };
            var repositoryMock = new Mock<IRepository<RequestEntry>>();
            repositoryMock
                .Setup(x => x.FindManyAsync(
                    It.IsAny<Expression<Func<RequestEntry, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedRequestEntries);
            var sut = new AllRequestEntriesQueryHandler(repositoryMock.Object);

            var actualResult = await sut.Handle(new AllRequestEntriesQuery(), CancellationToken.None);

            actualResult.Should()
                .BeOfType<AllRequestEntriesSuccessResult>()
                .Which.RequestEntries
                .Should().Contain(expectedRequestEntries);
        }
    }
}