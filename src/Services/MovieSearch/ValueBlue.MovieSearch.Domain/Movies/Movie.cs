using System;
using ValueBlue.MovieSearch.Domain.Movies.ValueObjects;

namespace ValueBlue.MovieSearch.Domain.Movies
{
    public abstract class Movie : Entity<MovieId>
    {
        public abstract SearchToken SearchToken { get; }
        public abstract ImdbId ImdbId { get; }
        public abstract ProcessingTime ProcessingTime { get; }
        public abstract DateTime Timestamp { get; }
        public abstract IpAddress IpAddress { get; }
    }
}