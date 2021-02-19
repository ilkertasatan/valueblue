namespace ValueBlue.MovieSearch.Domain
{
    public interface IHaveTheEntityId<TEntityId> where TEntityId : struct
    {
        public TEntityId Id { get; set; }
    }
}