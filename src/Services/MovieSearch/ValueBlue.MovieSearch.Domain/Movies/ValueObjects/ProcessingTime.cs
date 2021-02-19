using System;

namespace ValueBlue.MovieSearch.Domain.Movies.ValueObjects
{
    public readonly struct ProcessingTime :
        IHaveTheValueAs<TimeSpan>,
        IEquatable<ProcessingTime>
    {
        public ProcessingTime(TimeSpan processingTime)
        {
            Value = processingTime;
        }

        public bool Equals(ProcessingTime other)
        {
            return Value == other.Value;
        }

        public TimeSpan Value { get; }

        public override bool Equals(object obj)
        {
            return obj is ProcessingTime other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(ProcessingTime left, ProcessingTime right) => left.Equals(right);

        public static bool operator !=(ProcessingTime left, ProcessingTime right) => !(left == right);
    }
}