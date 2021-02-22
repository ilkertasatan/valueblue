using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ValueBlue.MovieSearch.Api.UseCases.V1.GetSingleRequestEntry;
using ValueBlue.MovieSearch.Application.UseCases.GetSingleRequestEntry;
using ValueBlue.MovieSearch.Domain.RequestEntries;
using Xunit;

namespace ValueBlue.MovieSearch.UnitTests.UseCaseTests.GetSingleRequestEntry
{
    public class RequestEntryControllerTests
    {
        [Fact]
        public async Task Should_Return_Single_Request_Entry()
        {
            var expectedRequestEntry = new RequestEntry("search-token", "imdbId", 100, DateTime.Now, "127.0.0.1");
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(x => x.Send(It.IsAny<GetSingleRequestEntryQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetSingleRequestEntrySuccessResult(expectedRequestEntry));
            var sut = new RequestEntryController(mediatorMock.Object);

            var actualResult = await sut.GetSingleRequestEntryAsync(Guid.NewGuid());

            actualResult.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should()
                .BeOfType<SingleRequestEntryResponse>();
        }
    }
}