using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using ValueBlue.MovieSearch.Domain.RequestEntries;
using ValueBlue.MovieSearch.Infrastructure.DataAccess.Repositories.Extensions;

namespace ValueBlue.MovieSearch.Infrastructure.DataAccess.Repositories
{
    public class UsageReportRepository :
        Repository<RequestEntry>,
        IUsageReportRepository
    {
        public UsageReportRepository(IMongoDatabase mongoDb) : base(mongoDb)
        {
        }

        public async Task<IEnumerable<UsageReport>> GroupByTimestampAsync(
            DateTime timestamp,
            CancellationToken cancellationToken)
        {
            var result = await Collection.Aggregate()
                .Match(r => r.Timestamp >= timestamp.ToQuery())
                .Group(
                    r => new DateTime(
                        r.Timestamp.Year,
                        r.Timestamp.Month,
                        r.Timestamp.Day,
                        r.Timestamp.Hour,
                        r.Timestamp.Minute,
                        0),
                    g => new {_id = g.Key, count = g.Count()}
                )
                .SortBy(d => d._id)
                .ToListAsync(cancellationToken: cancellationToken);

            return result
                .Select(r => new UsageReport(r._id.ToString("g"), r.count))
                .ToList();
        }
    }
}