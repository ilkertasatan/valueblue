using System;

namespace ValueBlue.MovieSearch.Domain.RequestEntries
{
    public abstract class RequestEntry :
        IAggregate<object>,
        IMaybeExist
    {
        public abstract object Id { get; protected set; }
        public abstract string SearchToken { get; protected set; }
        public abstract string ImdbId { get; protected set; }
        public abstract long ProcessingTime { get; protected set; }
        public abstract DateTime Timestamp { get; protected set; }
        public abstract string IpAddress { get; protected set; }

        public bool Exists()
        {
            return !Equals(Id, default);
        }
    }
}