using System;

namespace ValueBlue.MovieSearch.Domain.Movies.ValueObjects
{
    public class Language :
        ValueObject<Language>
    {
        private Language(string language)
        {
            Name = language;
        }

        public string Name { get; }

        public static Language New(string language)
        {
            return new Language(language);
        }

        protected override bool EqualsCore(Language other)
        {
            return Name == other.Name;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(base.GetHashCode(), Name);
        }
    }
}