using System;
using System.Threading.Tasks;
using FluentAssertions;
using ValueBlue.MovieSearch.Domain.Movies;
using ValueBlue.MovieSearch.Infrastructure.MovieServices.OmDb;
using Xunit;

namespace ValueBlue.MovieSearch.IntegrationTests.OmDbTests
{
    public class OmDbMovieServiceTests
    {
        [Fact]
        public async Task Should_Return_Movie_Given_Title_When_Api_Key_Is_Valid()
        {
            var uri = new Uri("http://www.omdbapi.com");
            const string apiKey = "e8ee624b";
            const string expectedMovieTitle = "Batman";

            var sut = new OmDbMovieService(uri, apiKey, new MovieTranslator());

            var actualResult = await sut.GetMovieByTitleAsync(expectedMovieTitle);
            actualResult.Should()
                .BeOfType<Movie>()
                .Which.Info.Title
                .Should().Be(expectedMovieTitle);
        }
    }
}