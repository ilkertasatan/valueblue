using System;

namespace ValueBlue.MovieSearch.Domain.Movies.ValueObjects
{
    public readonly struct IpAddress :
        IHaveTheValueAs<string>,
        IEquatable<IpAddress>
    {
        public IpAddress(string ipAddress)
        {
            Value = ipAddress;
        }

        public string Value { get; }

        public bool Equals(IpAddress other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is IpAddress other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
        
        public static bool operator ==(IpAddress left, IpAddress right) => left.Equals(right);

        public static bool operator !=(IpAddress left, IpAddress right) => !(left == right);
    }
}