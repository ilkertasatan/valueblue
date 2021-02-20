using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Domain;

namespace ValueBlue.MovieSearch.Application.UseCases.SearchMovie
{
    public class MovieSearchQueryHandler : 
        IRequestHandler<MovieSearchQuery, IQueryResult>
    {
        private readonly ISearchMovieByTitle _movieService;

        public MovieSearchQueryHandler(ISearchMovieByTitle movieService)
        {
            _movieService = movieService;
        }

        public async Task<IQueryResult> Handle(MovieSearchQuery request, CancellationToken cancellationToken)
        {
            var movie = await _movieService.GetMovieByTitleAsync(request.Title);
            if (!movie.Exists())
                return new MovieNotFoundResult($"Movie '{movie.Title}' not found!");
            
            return new MovieSearchSuccessResult(movie);
        }
    }
}