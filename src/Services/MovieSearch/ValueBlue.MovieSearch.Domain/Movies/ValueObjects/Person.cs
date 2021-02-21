namespace ValueBlue.MovieSearch.Domain.Movies.ValueObjects
{
    public class Person :
        ValueObject<Person>
    {
        private Person(string fullName)
        {
            FullName = fullName;
        }

        public string FullName { get; }

        public static Person New(string fullName)
        {
            return new Person(fullName);
        }
    }
}