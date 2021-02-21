using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;

namespace ValueBlue.MovieSearch.Application.UseCases.SearchMovie
{
    public sealed class MovieSearchQuery : 
        IRequest<IQueryResult>
    {
        public MovieSearchQuery(string movieTitle, string ipAddress)
        {
            MovieTitle = movieTitle;
            IpAddress = ipAddress;
        }

        public string MovieTitle { get; }
        public string IpAddress { get; }
    }
}