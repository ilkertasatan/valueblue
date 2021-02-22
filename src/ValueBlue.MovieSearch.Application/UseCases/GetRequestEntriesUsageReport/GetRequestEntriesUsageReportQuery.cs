using System;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;

namespace ValueBlue.MovieSearch.Application.UseCases.GetRequestEntriesUsageReport
{
    public sealed class GetRequestEntriesUsageReportQuery :
        IRequest<IQueryResult>
    {
        public GetRequestEntriesUsageReportQuery(DateTime timestamp)
        {
            Timestamp = timestamp;
        }

        public DateTime Timestamp { get; }
    }
}