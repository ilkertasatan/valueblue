using System;

namespace ValueBlue.MovieSearch.Domain.Movies
{
    public abstract class MovieSearchRequest : 
        IAggregate<object>,
        IMaybeExist
    {
        public abstract object Id { get; }
        public abstract string SearchToken { get; }
        public abstract string ImdbId { get; }
        public abstract long ProcessingTime { get; }
        public abstract DateTime Timestamp { get; }
        public abstract string IpAddress { get; }

        public bool Exists()
        {
            return !Equals(Id, default);
        }
    }
}