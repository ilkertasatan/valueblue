using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ValueBlue.MovieSearch.Api.UseCases.V1.GetRequestEntriesUsageReport;
using ValueBlue.MovieSearch.Application.UseCases.GetRequestEntriesUsageReport;
using ValueBlue.MovieSearch.Domain.RequestEntries;
using Xunit;

namespace ValueBlue.MovieSearch.UnitTests.UseCaseTests.GetRequestEntriesUsageReport
{
    public class RequestEntryControllerTests
    {
        [Fact]
        public async Task Should_Return_200_With_Usage_Report()
        {
            var expectedUsageReports = new[]
            {
                new UsageReport("2021-02-21", 5),
                new UsageReport("2021-02-22", 10)
            };
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(x => x.Send(It.IsAny<GetRequestEntriesUsageReportQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetRequestEntriesUsageReportSuccessResult(expectedUsageReports));
            var sut = new RequestEntryController(mediatorMock.Object);

            var actualResult = await sut.GetRequestEntriesUsageReportAsync(new GetRequestEntriesUsageReportRequest
            {
                Timestamp = new DateTime(2021, 02, 21)
            });

            actualResult.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should()
                .BeOfType<List<GetRequestEntriesUsageReportResponse>>();
        }
    }
}