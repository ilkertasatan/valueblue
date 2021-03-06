﻿using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using MediatR;
using Moq;
using ValueBlue.MovieSearch.Application.UseCases.SearchMovie;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.Movies;
using Xunit;

namespace ValueBlue.MovieSearch.UnitTests.UseCaseTests.SearchMovie
{
    public class SearchMovieQueryHandlerTests 
    {
        private readonly Mock<ISearchMovieByTitle> _movieServiceMock;
        private readonly SearchMovieQueryHandler _sut;

        public SearchMovieQueryHandlerTests()
        {
            _movieServiceMock = new Mock<ISearchMovieByTitle>();
            _sut = new SearchMovieQueryHandler(
                new Mock<IMediator>().Object,
                _movieServiceMock.Object);
        }

        [Fact]
        public async Task Should_Return_Success_Result_When_Movie_Is_Found()
        {
            var expectedMovie = GivenMovie();
            _movieServiceMock
                .Setup(x => x.GetMovieByTitleAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedMovie);

            var actualResult =
                await _sut.Handle(new SearchMovieQuery("movie-title", "ip-address"), CancellationToken.None);

            actualResult.Should()
                .BeOfType<SearchMovieSuccessResult>()
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
                await _sut.Handle(new SearchMovieQuery("movie-title", "ip-address"), CancellationToken.None);

            actualResult.Should().BeOfType<MovieNotFoundResult>();
        }
        
        private static Movie GivenMovie()
        {
            return new Fixture().Create<Movie>();
        }
    }
}