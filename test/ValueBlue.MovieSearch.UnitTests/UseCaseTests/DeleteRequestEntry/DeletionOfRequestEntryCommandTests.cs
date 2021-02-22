using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ValueBlue.MovieSearch.Application.Common.Model;
using ValueBlue.MovieSearch.Application.UseCases.DeleteRequestEntry;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.RequestEntries;
using Xunit;

namespace ValueBlue.MovieSearch.UnitTests.UseCaseTests.DeleteRequestEntry
{
    public class DeletionOfRequestEntryCommandTests
    {
        [Fact]
        public async Task Should_Delete_Request_Entry()
        {
            var repositoryMock = new Mock<IRepository<RequestEntry>>();
            repositoryMock
                .Setup(x => x.FindOneAsync(
                    It.IsAny<Expression<Func<RequestEntry, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RequestEntry("search-token", "imdbId", 100, DateTime.Now, "ip_address"));
            repositoryMock
                .Setup(x => x.DeleteOneAsync(
                    It.IsAny<Expression<Func<RequestEntry, bool>>>(),
                    It.IsAny<CancellationToken>()));
            var sut = new DeleteRequestEntryCommandHandler(repositoryMock.Object);

            var actualResult = await sut.Handle(new DeleteRequestEntryCommand(Guid.NewGuid()), CancellationToken.None);

            actualResult.Should().BeOfType<DeleteRequestEntrySuccessResult>();
        }

        [Fact]
        public async Task Should_Not_Delete_Request_Entry_When_Request_Entry_Is_Not_Found()
        {
            var repositoryMock = new Mock<IRepository<RequestEntry>>();
            repositoryMock
                .Setup(x => x.FindOneAsync(
                    It.IsAny<Expression<Func<RequestEntry, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(RequestEntry.Empty);
            repositoryMock
                .Setup(x => x.DeleteOneAsync(
                    It.IsAny<Expression<Func<RequestEntry, bool>>>(),
                    It.IsAny<CancellationToken>()));
            var sut = new DeleteRequestEntryCommandHandler(repositoryMock.Object);

            var actualResult = await sut.Handle(new DeleteRequestEntryCommand(Guid.NewGuid()), CancellationToken.None);

            actualResult.Should().BeOfType<RequestEntryNotFoundResult>();
        }
    }
}