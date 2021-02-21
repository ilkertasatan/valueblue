using System.Collections.Generic;

namespace ValueBlue.MovieSearch.Domain.Movies.ValueObjects
{
    public class MovieInfo :
        ValueObject<MovieInfo>
    {
        private MovieInfo(
            string title,
            string year,
            string rated,
            string runtime,
            IEnumerable<Genre> genres,
            string released,
            string country)
        {
            Title = title;
            Year = year;
            Rated = rated;
            Runtime = runtime;
            Genres = genres;
            Released = released;
            Country = country;
        }

        public string Title { get; }
        public string Year { get; }
        public string Rated { get; }
        public string Runtime { get; }
        public IEnumerable<Genre> Genres { get; }
        public string Released { get; }
        public string Country { get; }

        public static MovieInfo New(
            string title,
            string year,
            string rated,
            string runtime,
            IEnumerable<Genre> genres,
            string released,
            string country)
        {
            return new MovieInfo(
                title,
                year,
                rated,
                runtime,
                genres,
                released,
                country);
        }
    }
}