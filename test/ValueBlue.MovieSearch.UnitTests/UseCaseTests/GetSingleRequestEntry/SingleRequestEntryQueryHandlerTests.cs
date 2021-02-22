using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ValueBlue.MovieSearch.Application.Common.Model;
using ValueBlue.MovieSearch.Application.UseCases.GetAllRequestEntries;
using ValueBlue.MovieSearch.Application.UseCases.GetSingleRequestEntry;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.RequestEntries;
using Xunit;

namespace ValueBlue.MovieSearch.UnitTests.UseCaseTests.GetSingleRequestEntry
{
    public class SingleRequestEntryQueryHandlerTests
    {
        private readonly Mock<IRepository<RequestEntry>> _repositoryMock;
        private readonly GetSingleRequestEntryQueryHandler _sut;

        public SingleRequestEntryQueryHandlerTests()
        {
            _repositoryMock = new Mock<IRepository<RequestEntry>>();
            _sut = new GetSingleRequestEntryQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Should_Return_Single_Request_Entry_Success_Result_When_Request_Entry_Is_Found()
        {
            var expectedRequestEntry = new RequestEntry("search-token", "imdbId", 100, DateTime.Now, "127.0.0.1");
            _repositoryMock
                .Setup(x => x.FindOneAsync(
                    It.IsAny<Expression<Func<RequestEntry, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedRequestEntry);

            var actualResult = await _sut.Handle(new GetSingleRequestEntryQuery(Guid.NewGuid()), CancellationToken.None);

            actualResult.Should()
                .BeOfType<GetSingleRequestEntrySuccessResult>()
                .Which.RequestEntry
                .Should().BeEquivalentTo(expectedRequestEntry);
        }
        
        [Fact]
        public async Task Should_Return_Request_Entry_Not_Found_Result_When_Request_Entry_Is_Not_Found()
        {
            _repositoryMock
                .Setup(x => x.FindOneAsync(
                    It.IsAny<Expression<Func<RequestEntry, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(RequestEntry.Empty);
            
            var actualResult = await _sut.Handle(new GetSingleRequestEntryQuery(Guid.NewGuid()), CancellationToken.None);

            actualResult.Should().BeOfType<RequestEntryNotFoundResult>();
        }
    }
}