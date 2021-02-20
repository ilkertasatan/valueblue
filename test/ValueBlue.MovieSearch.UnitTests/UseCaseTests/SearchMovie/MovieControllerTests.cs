using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ValueBlue.MovieSearch.Api.UseCases.V1.SearchMovie;
using ValueBlue.MovieSearch.Application.UseCases.SearchMovie;
using Xunit;

namespace ValueBlue.MovieSearch.UnitTests.UseCaseTests.SearchMovie
{
    public class MovieControllerTests :
        IClassFixture<StandardFixture>
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly MovieController _sut;
        private readonly StandardFixture _fixture;

        public MovieControllerTests(StandardFixture fixture)
        {
            _fixture = fixture;
            _mediatorMock = new Mock<IMediator>();
            _sut = new MovieController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Should_Return_200_When_Movie_Is_Found()
        {
            var expectedMovie = _fixture.GivenMovie();
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<MovieSearchQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new MovieSearchSuccessResult(expectedMovie));

            var actualResult = await _sut.SearchMovieByTitleAsync("movie-title");

            actualResult.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value
                .Should().BeOfType<MovieSearchByTitleResponse>()
                .Which
                .Should().BeEquivalentTo(expectedMovie);
        }

        [Fact]
        public async Task Should_Return_404_When_Movie_Is_Not_Found()
        {
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<MovieSearchQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new MovieNotFoundResult("message"));

            var actualResult = await _sut.SearchMovieByTitleAsync("movie-title");

            actualResult.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}