namespace ValueBlue.MovieSearch.Domain.Movies.ValueObjects
{
    public class Poster :
        ValueObject<Poster>
    {
        private Poster(string url)
        {
            Url = url;
        }

        public string Url { get; }

        public static Poster New(string posterUrl)
        {
            return new Poster(posterUrl);
        }
    }
}