using MediatR;
using ValueBlue.MovieSearch.Application.Common.Interfaces;

namespace ValueBlue.MovieSearch.Application.UseCases.SearchMovie
{
    public sealed class MovieSearchQuery : 
        IRequest<IQueryResult>
    {
        public MovieSearchQuery(string title)
        {
            Title = title;
        }

        public string Title { get; }
    }
}