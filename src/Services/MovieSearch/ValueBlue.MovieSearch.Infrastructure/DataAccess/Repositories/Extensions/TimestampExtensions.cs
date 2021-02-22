using System;

namespace ValueBlue.MovieSearch.Infrastructure.DataAccess.Repositories.Extensions
{
    public static class TimestampExtensions
    {
        public static DateTime ToQuery(this DateTime timestamp)
        {
            var dateTime = new DateTime(
                timestamp.Year,
                timestamp.Month,
                timestamp.Day,
                timestamp.Hour,
                timestamp.Minute,
                0,
                DateTimeKind.Utc);
            return dateTime;
        }
    }
}