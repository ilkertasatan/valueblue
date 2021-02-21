using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Flurl.Http.Testing;
using ValueBlue.MovieSearch.Domain.Movies;
using ValueBlue.MovieSearch.Infrastructure.MovieServices.OmDb;
using ValueBlue.MovieSearch.UnitTests.UseCaseTests;
using Xunit;

namespace ValueBlue.MovieSearch.UnitTests.InfrastructureTests.MovieServices.OmDb
{
    public class OmDbMovieServiceTests : 
        IClassFixture<StandardFixture>
    {
        private const int Once = 1;

        [Fact]
        public async Task Should_Call_Api_Given_Movie_Title_And_Api_Key()
        {
            var expectedResponse = new Fixture().Create<OmDbMovieResponse>();
            var movieTranslator = new MovieTranslator();
            var expectedMovie = movieTranslator.Translate(expectedResponse);
            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(expectedResponse);
            var sut = new OmDbMovieService(new Uri("http://fake-url.com/"), "fake-api-key", movieTranslator);

            var actualResult = await sut.GetMovieByTitleAsync("movie-title");

            actualResult
                .Should()
                .NotBeNull()
                .And
                .BeOfType<Movie>()
                .Which.Should().BeEquivalentTo(expectedMovie);
            httpTest
                .ShouldHaveCalled("http://fake-url.com/*")
                .WithVerb(HttpMethod.Get)
                .WithQueryParams(new
                {
                    t = "movie-title",
                    apikey = "fake-api-key"
                })
                .Times(Once);
        }
    }
}