using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ValueBlue.MovieSearch.Api.UseCases.V1.GetSingleRequestEntry;
using ValueBlue.MovieSearch.Application.Common.Model;
using ValueBlue.MovieSearch.Application.UseCases.GetAllRequestEntries;
using ValueBlue.MovieSearch.Domain.RequestEntries;
using Xunit;
using RequestEntryController = ValueBlue.MovieSearch.Api.UseCases.V1.GetAllRequestEntries.RequestEntryController;

namespace ValueBlue.MovieSearch.UnitTests.UseCaseTests.GetAllRequestEntries
{
    public class RequestEntryControllerTests
    {
        [Fact]
        public async Task Should_Return_200_With_Request_Entries()
        {
            var expectedRequestEntries = new[]
            {
                new RequestEntry("search-token", "imdbId", 100, DateTime.Now, "ip_address")
                {
                    Id = Guid.NewGuid().ToString()
                }
            };
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(x => x.Send(It.IsAny<GetAllRequestEntriesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetRequestEntriesSuccessResult(expectedRequestEntries));
            var sut = new RequestEntryController(mediatorMock.Object);

            var actualResult = await sut.GetAllRequestEntriesAsync();

            actualResult.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should()
                .BeOfType<List<SingleRequestEntryResponse>>();
        }
    }
}