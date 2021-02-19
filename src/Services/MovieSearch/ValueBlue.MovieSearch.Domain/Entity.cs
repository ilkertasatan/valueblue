namespace ValueBlue.MovieSearch.Domain
{
    public abstract class Entity<TEntityId> :
        IHaveTheEntityId<TEntityId>,
        IMaybeExist
        where TEntityId : struct
    {
        public TEntityId Id { get; set; }
        
        public bool Exists() => !Equals(Id, default(TEntityId));
    }
}