namespace ValueBlue.MovieSearch.Domain.Movies.ValueObjects
{
    public class Awards :
        ValueObject<Awards>
    {
        private Awards(string award)
        {
            Name = award;
        }

        public string Name { get; }

        public static Awards New(string award)
        {
            return new Awards(award);
        }
    }
}