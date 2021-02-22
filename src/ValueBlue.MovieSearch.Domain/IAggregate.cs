namespace ValueBlue.MovieSearch.Domain
{
    public interface IAggregate<out TEntityId>
    {
        public TEntityId Id { get; }
    }
}