using ValueBlue.MovieSearch.Domain.Movies;

namespace ValueBlue.MovieSearch.Domain
{
    public interface ITranslateMovie<in T>
    {
        Movie Translate(T @object);
    }
}