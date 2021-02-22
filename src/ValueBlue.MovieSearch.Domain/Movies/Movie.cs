using System.Collections.Generic;
using ValueBlue.MovieSearch.Domain.Movies.ValueObjects;

namespace ValueBlue.MovieSearch.Domain.Movies
{
    public class Movie :
        IMaybeExist
    {
        public static readonly Movie Empty = new Movie();

        private Movie()
        {
        }

        private Movie(
            MovieInfo info,
            IEnumerable<Person> directors,
            IEnumerable<Person> writers,
            IEnumerable<Person> actors,
            Plot plot,
            Language language,
            Awards awards,
            Poster poster,
            ImdbInfo imdb)
        {
            Info = info;
            Directors = directors;
            Writers = writers;
            Actors = actors;
            Plot = plot;
            Language = language;
            Awards = awards;
            Poster = poster;
            Imdb = imdb;
        }

        public MovieInfo Info { get; }
        public IEnumerable<Person> Directors { get; }
        public IEnumerable<Person> Writers { get; }
        public IEnumerable<Person> Actors { get; }
        public Plot Plot { get; }
        public Language Language { get; }
        public Awards Awards { get; }
        public Poster Poster { get; }
        public ImdbInfo Imdb { get; }

        public bool Exists()
        {
            return !Equals(Info, default(MovieInfo));
        }

        public static Movie New(
            MovieInfo info,
            IEnumerable<Person> directors,
            IEnumerable<Person> writers,
            IEnumerable<Person> actors,
            Plot plot,
            Language language,
            Awards awards,
            Poster poster,
            ImdbInfo imdb)
        {
            return new Movie(
                info,
                directors,
                writers,
                actors,
                plot,
                language,
                awards,
                poster,
                imdb);
        }
    }
}