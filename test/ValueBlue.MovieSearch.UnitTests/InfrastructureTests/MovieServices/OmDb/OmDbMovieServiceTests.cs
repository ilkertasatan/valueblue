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
    public class OmDbMovieServiceTests
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

        [Fact]
        public void Should_Throw_Exception_When_Uri_Is_Null()
        {
            Action act = () =>
            {
                new OmDbMovieService(null, "fake-api-key", new MovieTranslator())
                    .GetMovieByTitleAsync("movie-title");
            };
            
            act.Should().Throw<Exception>();
        }
        
        [Fact]
        public void Should_Throw_Exception_When_Uri_Is_Invalid()
        {
            Action act = () =>
            {
                new OmDbMovieService(new Uri("invalid"), "fake-api-key", new MovieTranslator())
                    .GetMovieByTitleAsync("movie-title");
            };
            
            act.Should().Throw<Exception>();
        }
        
        [Fact]
        public void Should_Throw_Exception_When_Api_Key_Is_Null_Or_Empty()
        {
            Action act = () =>
            {
                new OmDbMovieService(new Uri("http://fake-url.com/"), "", new MovieTranslator())
                    .GetMovieByTitleAsync("movie-title");
            };
            
            act.Should().Throw<Exception>();
        }
    }
}