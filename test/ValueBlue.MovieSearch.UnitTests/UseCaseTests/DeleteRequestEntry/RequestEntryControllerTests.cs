using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ValueBlue.MovieSearch.Api.UseCases.V1.DeleteRequestEntry;
using ValueBlue.MovieSearch.Application.UseCases.DeleteRequestEntry;
using Xunit;

namespace ValueBlue.MovieSearch.UnitTests.UseCaseTests.DeleteRequestEntry
{
    public class RequestEntryControllerTests
    {
        [Fact]
        public async Task Should_Return_204_When_Request_Entry_Is_Deleted()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(x => x.Send(It.IsAny<DeleteRequestEntryCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DeleteRequestEntrySuccessResult());
            var sut = new RequestEntryController(mediatorMock.Object);

            var actualResult = await sut.DeleteRequestEntryAsync(Guid.NewGuid().ToString());

            actualResult.Should().BeOfType<NoContentResult>();
        }
    }
}