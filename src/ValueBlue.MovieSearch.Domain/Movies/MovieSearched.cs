using System;
using MediatR;

namespace ValueBlue.MovieSearch.Domain.Movies
{
    public class MovieSearched :
        INotification
    {
        public MovieSearched(
            string searchToken,
            string imdbId,
            long processingTime,
            string ipAddress)
        {
            SearchToken = searchToken;
            ImdbId = imdbId;
            ProcessingTime = processingTime;
            Timestamp = DateTime.UtcNow;
            IpAddress = ipAddress;
        }

        public string SearchToken { get; }
        public string ImdbId { get; }
        public long ProcessingTime { get; }
        public DateTime Timestamp { get; }
        public string IpAddress { get; }
    }
}