using System.Collections.Generic;
using System.Linq;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.Movies;
using ValueBlue.MovieSearch.Domain.Movies.ValueObjects;

namespace ValueBlue.MovieSearch.Infrastructure.MovieServices.OmDb
{
    public class MovieTranslator :
        ITranslateMovie<OmDbMovieResponse>
    {
        public Movie Translate(OmDbMovieResponse @object)
        {
            if (@object == null)
                return Movie.Empty;

            var movieInfo = MovieInfo.New(
                @object.Title,
                @object.Year,
                @object.Rated,
                @object.Runtime,
                TranslateGenres(@object.Genre),
                @object.Released,
                @object.Country);

            var directors = TranslateDirectors(@object.Director);
            var writers = TranslateWriters(@object.Writer);
            var actors = TranslateActors(@object.Actors);

            var plot = Plot.New(@object.Plot);
            var language = Language.New(@object.Language);
            var awards = Awards.New(@object.Awards);
            var poster = Poster.New(@object.Poster);
            var imdbInfo = ImdbInfo.New(@object.ImdbId, @object.ImdbVotes, @object.ImdbRating);

            return Movie.New(
                movieInfo,
                directors,
                writers,
                actors,
                plot,
                language,
                awards,
                poster,
                imdbInfo);
        }

        private static IEnumerable<Genre> TranslateGenres(string genre)
        {
            var genres = new List<Genre>();
            if (string.IsNullOrWhiteSpace(genre))
                return genres;

            if (genre.Contains(','))
                genres = genre
                    .Split(',')
                    .Select(Genre.New)
                    .ToList();
            else
                genres.Add(Genre.New(genre));

            return genres;
        }

        private static IEnumerable<Person> TranslatePerson(string person)
        {
            var persons = new List<Person>();
            if (string.IsNullOrWhiteSpace(person))
                return persons;

            if (person.Contains(','))
            {
                persons = person
                    .Split(',')
                    .Select(Person.New)
                    .ToList();
            }
            else
            {
                persons.Add(Person.New(person));
            }

            return persons;
        }

        private static IEnumerable<Person> TranslateDirectors(string directors)
            => TranslatePerson(directors);

        private static IEnumerable<Person> TranslateWriters(string writers)
            => TranslatePerson(writers);

        private static IEnumerable<Person> TranslateActors(string actors)
            => TranslatePerson(actors);
    }
}