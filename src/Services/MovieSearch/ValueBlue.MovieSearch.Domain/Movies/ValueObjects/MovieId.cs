using System;

namespace ValueBlue.MovieSearch.Domain.Movies.ValueObjects
{
    public readonly struct MovieId :
        IHaveTheValueAs<Guid>,
        IEquatable<MovieId>
    {
        public MovieId(Guid id)
        {
            Value = id;
        }
        
        public Guid Value { get; }
        
        public bool Equals(MovieId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is MovieId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(MovieId left, MovieId right) => left.Equals(right);

        public static bool operator !=(MovieId left, MovieId right) => !(left == right);
    }
}