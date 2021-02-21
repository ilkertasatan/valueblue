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
    }
}