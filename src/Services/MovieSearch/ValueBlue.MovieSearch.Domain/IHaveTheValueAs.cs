namespace ValueBlue.MovieSearch.Domain
{
    public interface IHaveTheValueAs<out T>
    {
        T Value { get; }
    }
}