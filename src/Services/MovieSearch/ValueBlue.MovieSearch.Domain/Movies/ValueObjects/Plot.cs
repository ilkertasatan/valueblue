using System;

namespace ValueBlue.MovieSearch.Domain.Movies.ValueObjects
{
    public class Plot :
        ValueObject<Plot>
    {
        private Plot(string plot)
        {
            Value = plot;
        }

        public string Value { get; }

        public static Plot New(string plot)
        {
            return new Plot(plot);
        }

        protected override bool EqualsCore(Plot other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(base.GetHashCode(), Value);
        }
    }
}