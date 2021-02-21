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
    }
}