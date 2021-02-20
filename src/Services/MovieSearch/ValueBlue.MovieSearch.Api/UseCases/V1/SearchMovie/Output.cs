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
                Title = movie.Title,
                Year = movie.Year,
                Rated = movie.Rated,
                Released = movie.Released,
                Runtime = movie.Runtime,
                Genre = movie.Genre,
                Director = movie.Director,
                Writer = movie.Writer,
                Actors = movie.Actors,
                Plot = movie.Plot,
                Language = movie.Language,
                Country = movie.Country,
                Awards = movie.Awards,
                Poster = movie.Poster,
                ImdbRating = movie.ImdbRating,
                ImdbVotes = movie.ImdbVotes,
                ImdbId = movie.ImdbId
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