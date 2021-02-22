using System;

namespace ValueBlue.MovieSearch.Domain.RequestEntries
{
    public class RequestEntry :
        IAggregate<string>,
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
            SearchToken = searchToken;
            ImdbId = imdbId;
            ProcessingTime = processingTime;
            Timestamp = timestamp;
            IpAddress = ipAddress;
        }

        public string Id { get; set; }
        public string SearchToken { get; private set; }
        public string ImdbId { get; private set; }
        public long ProcessingTime { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string IpAddress { get; private set; }

        public bool Exists()
        {
            return !string.IsNullOrWhiteSpace(Id);
        }
    }
}