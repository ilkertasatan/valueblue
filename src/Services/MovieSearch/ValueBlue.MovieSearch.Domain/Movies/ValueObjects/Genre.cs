namespace ValueBlue.MovieSearch.Domain.Movies.ValueObjects
{
    public class Genre :
        ValueObject<Genre>
    {
        private Genre(string genre)
        {
            Name = genre;
        }

        public string Name { get; }

        public static Genre New(string genre)
        {
            return new Genre(genre);
        }
    }
}