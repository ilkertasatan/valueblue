using System.Threading.Tasks;
using ValueBlue.MovieSearch.Domain.Movies;

namespace ValueBlue.MovieSearch.Domain
{
    public interface ISearchMovieByTitle
    {
        Task<Movie> GetMovieByTitleAsync(string title);
    }
}