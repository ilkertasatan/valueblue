using System;

namespace ValueBlue.MovieSearch.Domain.Movies.ValueObjects
{
    public readonly struct ImdbId :
        IHaveTheValueAs<string>,
        IEquatable<ImdbId>
    {
        public ImdbId(string id)
        {
            Value = id;
        }

        public bool Equals(ImdbId other)
        {
            return Value == other.Value;
        }

        public string Value { get; }

        public override bool Equals(object obj)
        {
            return obj is ImdbId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(ImdbId left, ImdbId right) => left.Equals(right);

        public static bool operator !=(ImdbId left, ImdbId right) => !(left == right);
    }
}