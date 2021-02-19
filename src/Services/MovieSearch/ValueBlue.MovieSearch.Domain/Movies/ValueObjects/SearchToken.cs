using System;

namespace ValueBlue.MovieSearch.Domain.Movies.ValueObjects
{
    public readonly struct SearchToken :
        IHaveTheValueAs<string>,
        IEquatable<SearchToken>
    {
        public SearchToken(string searchToken)
        {
            Value = searchToken;
        }

        public string Value { get; }

        public bool Equals(SearchToken other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is SearchToken other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
        
        public static bool operator ==(SearchToken left, SearchToken right) => left.Equals(right);

        public static bool operator !=(SearchToken left, SearchToken right) => !(left == right);
    }
}