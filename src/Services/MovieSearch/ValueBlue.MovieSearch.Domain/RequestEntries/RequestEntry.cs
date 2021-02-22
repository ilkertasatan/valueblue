using System;
using ValueBlue.MovieSearch.Domain.Movies.ValueObjects;

namespace ValueBlue.MovieSearch.Domain.RequestEntries
{
    public class RequestEntry :
        IAggregate<Guid>,
        IMaybeExist
    {
        public static readonly RequestEntry Empty = new RequestEntry();

        public RequestEntry()
        {
        }

        public RequestEntry(
            string searchToken,
            string imdbId,
            long processingTime,
            DateTime timestamp,
            string ipAddress)
        {
            Id = Guid.NewGuid();
            SearchToken = searchToken;
            ImdbId = imdbId;
            ProcessingTime = processingTime;
            Timestamp = timestamp;
            IpAddress = ipAddress;
        }

        public Guid Id { get; private set; }
        public string SearchToken { get; private set; }
        public string ImdbId { get; private set; }
        public long ProcessingTime { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string IpAddress { get; private set; }

        public bool Exists()
        {
            return !Equals(Id, Guid.Empty);
        }
    }
}