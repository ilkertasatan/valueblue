using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ValueBlue.MovieSearch.Domain.RequestEntries
{
    public interface IUsageReportRepository
    {
        Task<IEnumerable<UsageReport>> GroupByTimestampAsync(
            DateTime timestamp,
            CancellationToken cancellationToken);
    }
}