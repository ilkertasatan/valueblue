using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using ValueBlue.MovieSearch.Application.UseCases.SearchMovie;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.Movies;
using Xunit;

namespace ValueBlue.MovieSearch.UnitTests.UseCaseTests.SearchMovie
{
    public class MovieSearchQueryHandlerTests :
        IClassFixture<StandardFixture>
    {
        private readonly StandardFixture _fixture;
        private readonly Mock<ISearchMovieByTitle> _movieServiceMock;
        private readonly MovieSearchQueryHandler _sut;

        public MovieSearchQueryHandlerTests(StandardFixture fixture)
        {
            _fixture = fixture;
            _movieServiceMock = new Mock<ISearchMovieByTitle>();
            _sut = new MovieSearchQueryHandler(
                new Mock<IMediator>().Object,
                _movieServiceMock.Object);
        }

        [Fact]
        public async Task Should_Return_Success_Result_When_Movie_Is_Found()
        {
            var expectedMovie = _fixture.GivenMovie();
            _movieServiceMock
                .Setup(x => x.GetMovieByTitleAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedMovie);

            var actualResult =
                await _sut.Handle(new MovieSearchQuery("movie-title", "ip-address"), CancellationToken.None);

            actualResult.Should()
                .BeOfType<MovieSearchSuccessResult>()
                .Which.Movie
                .Should().BeEquivalentTo(expectedMovie);
        }

        [Fact]
        public async Task Should_Return_Not_Found_Result_When_Movie_Is_Not_Found()
        {
            _movieServiceMock
                .Setup(x => x.GetMovieByTitleAsync(It.IsAny<string>()))
                .ReturnsAsync(Movie.Empty);

            var actualResult =
                await _sut.Handle(new MovieSearchQuery("movie-title", "ip-address"), CancellationToken.None);

            actualResult.Should().BeOfType<MovieNotFoundResult>();
        }
    }
}