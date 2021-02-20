using AutoFixture;
using ValueBlue.MovieSearch.Domain.Movies;

namespace ValueBlue.MovieSearch.UnitTests.UseCaseTests
{
    public class StandardFixture
    {
        public Movie GivenMovie()
        {
            return new Fixture().Create<Movie>();
        }
    }
}