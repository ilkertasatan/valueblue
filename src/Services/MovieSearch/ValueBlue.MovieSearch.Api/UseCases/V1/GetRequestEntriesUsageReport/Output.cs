using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Application.UseCases.GetRequestEntriesUsageReport;
using ValueBlue.MovieSearch.Domain.RequestEntries;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.GetRequestEntriesUsageReport
{
    public static class Output
    {
        public static IActionResult For(IQueryResult output) =>
            output switch
            {
                GetRequestEntriesUsageReportSuccessResult result => Ok(result.UsageReports),
                _ => InternalServerError()
            };

        private static IActionResult Ok(IEnumerable<UsageReport> usageReports)
        {
            return new OkObjectResult(usageReports
                .Select(usage => new GetRequestEntriesUsageReportResponse
                {
                    Timestamp = usage.Timestamp,
                    Count = usage.Count
                })
                .ToList());
        }

        private static IActionResult InternalServerError()
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}