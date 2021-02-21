using System;

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

        protected override bool EqualsCore(Genre other)
        {
            return Name == other.Name;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(base.GetHashCode(), Name);
        }
    }
}