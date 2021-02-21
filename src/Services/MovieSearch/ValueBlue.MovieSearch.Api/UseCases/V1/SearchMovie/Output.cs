using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Application.UseCases.SearchMovie;
using ValueBlue.MovieSearch.Domain.Movies;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.SearchMovie
{
    public static class Output
    {
        public static IActionResult For(IQueryResult output) =>
            output switch
            {
                MovieSearchSuccessResult result => Ok(result.Movie),
                MovieNotFoundResult result => NotFound(result),
                _ => InternalServerError()
            };

        private static IActionResult Ok(Movie movie)
        {
            return new OkObjectResult(new MovieSearchByTitleResponse
            {
                Title = movie.Info.Title,
                Year = movie.Info.Year,
                Rated = movie.Info.Rated,
                Released = movie.Info.Released,
                Runtime = movie.Info.Runtime,
                Genre = string.Join(", ", movie.Info.Genres.Select(g => g.Name)),
                Director = string.Join(", ", movie.Directors.Select(p => p.FullName)),
                Writer = string.Join(", ", movie.Writers.Select(p => p.FullName)),
                Actors = string.Join(", ", movie.Actors.Select(p => p.FullName)),
                Plot = movie.Plot.Value,
                Language = movie.Language.Name,
                Country = movie.Info.Country,
                Awards = movie.Awards.Name,
                Poster = movie.Poster.Url,
                ImdbId = movie.Imdb.ImdbId,
                ImdbRating = movie.Imdb.Rating,
                ImdbVotes = movie.Imdb.Votes,
            });
        }

        private static IActionResult NotFound(MovieNotFoundResult message)
        {
            return new NotFoundObjectResult(message);
        }

        private static IActionResult InternalServerError()
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}