using System.Collections.Generic;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Domain.RequestEntries;

namespace ValueBlue.MovieSearch.Application.UseCases.GetRequestEntriesUsageReport
{
    public class GetRequestEntriesUsageReportSuccessResult : IQueryResult
    {
        public GetRequestEntriesUsageReportSuccessResult(
            IEnumerable<UsageReport> usageReports)
        {
            UsageReports = usageReports;
        }

        public IEnumerable<UsageReport> UsageReports { get; }
    }
}