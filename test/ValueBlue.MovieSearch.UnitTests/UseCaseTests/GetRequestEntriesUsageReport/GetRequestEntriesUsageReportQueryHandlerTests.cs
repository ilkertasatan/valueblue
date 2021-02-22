using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ValueBlue.MovieSearch.Application.UseCases.GetRequestEntriesUsageReport;
using ValueBlue.MovieSearch.Domain.RequestEntries;
using Xunit;

namespace ValueBlue.MovieSearch.UnitTests.UseCaseTests.GetRequestEntriesUsageReport
{
    public class GetRequestEntriesUsageReportQueryHandlerTests
    {
        [Fact]
        public async Task Should_Return_Request_Entries_Usage_Report()
        {
            var expectedUsageReports = new[]
            {
                new UsageReport("2021-02-21", 5),
                new UsageReport("2021-02-22", 10)
            };
            var repositoryMock = new Mock<IUsageReportRepository>();
            repositoryMock
                .Setup(x => x.GroupByTimestampAsync(It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUsageReports);
            var sut = new GetRequestEntriesUsageReportQueryHandler(repositoryMock.Object);

            var actualResult = await sut.Handle(
                new GetRequestEntriesUsageReportQuery(DateTime.Now), CancellationToken.None);

            actualResult.Should()
                .BeOfType<GetRequestEntriesUsageReportSuccessResult>()
                .Which.UsageReports
                .Should().Contain(expectedUsageReports);
        }
    }
}