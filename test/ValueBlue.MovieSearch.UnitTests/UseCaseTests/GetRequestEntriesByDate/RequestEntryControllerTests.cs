using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ValueBlue.MovieSearch.Api.UseCases.V1.GetRequestEntriesByDate;
using ValueBlue.MovieSearch.Api.UseCases.V1.GetSingleRequestEntry;
using ValueBlue.MovieSearch.Application.Common.Model;
using ValueBlue.MovieSearch.Application.UseCases.GetRequestEntriesByDate;
using ValueBlue.MovieSearch.Domain.RequestEntries;
using Xunit;
using RequestEntryController = ValueBlue.MovieSearch.Api.UseCases.V1.GetRequestEntriesByDate.RequestEntryController;

namespace ValueBlue.MovieSearch.UnitTests.UseCaseTests.GetRequestEntriesByDate
{
    public class RequestEntryControllerTests
    {
        [Fact]
        public async Task Should_Return_200_With_Request_Entries_Given_Date_Period()
        {
            var expectedRequestEntries = new[]
            {
                new RequestEntry("search-token", "imdbId", 100, DateTime.Now.AddDays(-1), "127.0.0.1"),
                new RequestEntry("search-token", "imdbId", 100, DateTime.Now, "127.0.0.1")
            };
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(x => x.Send(It.IsAny<GetRequestEntriesByDateQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetRequestEntriesSuccessResult(expectedRequestEntries));
            var sut = new RequestEntryController(mediatorMock.Object);

            var actualResult = await sut.GetRequestEntriesByDateAsync(new GetRequestEntriesByDateRequest
            {
                From = DateTime.Now.AddDays(-1),
                End = DateTime.Now
            });

            actualResult.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should()
                .BeOfType<List<SingleRequestEntryResponse>>();
        }
    }
}