using AutoFixture;
using FluentAssertions;
using ValueBlue.MovieSearch.Domain.Movies;
using ValueBlue.MovieSearch.Infrastructure.MovieServices.OmDb;
using Xunit;

namespace ValueBlue.MovieSearch.UnitTests.InfrastructureTests.MovieServices.OmDb
{
    public class MovieTranslatorTests
    {
        private MovieTranslator _sut;

        public MovieTranslatorTests()
        {
            _sut = new MovieTranslator();
        }
        
        [Fact]
        public void Should_Translate_Given_OmDb_Movie_Response()
        {
            var expectedResponse = new Fixture().Create<OmDbMovieResponse>();

            var actualResult = _sut.Translate(expectedResponse);

            actualResult.Should().BeOfType<Movie>();
        }


        [Fact]
        public void Should_Not_Translate_When_OmDb_Movie_Response_Is_Null()
        {
            _sut = new MovieTranslator();
            
            var actualResult = _sut.Translate(null);

            actualResult.Should()
                .BeOfType<Movie>()
                .Which.Should()
                .Be(Movie.Empty);
        }
    }
}