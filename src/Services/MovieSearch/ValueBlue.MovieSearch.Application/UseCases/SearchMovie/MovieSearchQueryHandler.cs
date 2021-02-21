using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.Movies;

namespace ValueBlue.MovieSearch.Application.UseCases.SearchMovie
{
    public class MovieSearchQueryHandler :
        IRequestHandler<MovieSearchQuery, IQueryResult>
    {
        private readonly IMediator _mediator;
        private readonly ISearchMovieByTitle _movieService;

        public MovieSearchQueryHandler(
            IMediator mediator,
            ISearchMovieByTitle movieService)
        {
            _mediator = mediator;
            _movieService = movieService;
        }

        public async Task<IQueryResult> Handle(MovieSearchQuery request, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();
            var movie = await _movieService.GetMovieByTitleAsync(request.MovieTitle);
            stopwatch.Stop();

            if (!movie.Exists())
                return new MovieNotFoundResult($"Movie '{request.MovieTitle}' not found!");

            var since = stopwatch.ElapsedMilliseconds;
            await _mediator.Publish(
                new MovieSearched(
                    movie.Info.Title,
                    movie.Imdb.ImdbId,
                    since,
                    request.IpAddress),
                cancellationToken);

            return new MovieSearchSuccessResult(movie);
        }
    }
}