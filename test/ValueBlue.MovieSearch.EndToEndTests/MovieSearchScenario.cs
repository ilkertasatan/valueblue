using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using ValueBlue.MovieSearch.Api.UseCases.V1.GetSingleRequestEntry;
using ValueBlue.MovieSearch.Api.UseCases.V1.SearchMovie;
using Xbehave;
using Xunit;

namespace ValueBlue.MovieSearch.EndToEndTests
{
    public class MovieSearchScenario : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;
        private readonly CancellationTokenSource _cancellation;

        public MovieSearchScenario(TestServerFixture fixture)
        {
            _cancellation = new CancellationTokenSource();
            _cancellation.CancelAfter(TimeSpan.FromSeconds(30));

            _fixture = fixture;
        }

        [Scenario]
        public void Should_Search_A_Movie(string movieTitle)
        {
            "Given I have a movie title".x(() => movieTitle = "Terminator");

            "When movie is found".x(async () => movieTitle = await SearchMovieAsync(movieTitle));

            "Then request entry is persisted".x(async () => await AssertRequestEntryIsPersisted(movieTitle));
        }

        private async Task<string> SearchMovieAsync(string movieTitle)
        {
            var response = await _fixture
                .GetAsync<MovieSearchByTitleResponse>($"api/v1/movies?t={movieTitle}", _cancellation.Token);

            return response.Title;
        }

        private async Task AssertRequestEntryIsPersisted(string movieTitle)
        {
            var requestEntries = await _fixture
                .GetAsync<IEnumerable<SingleRequestEntryResponse>>("api/v1/request-entries", _cancellation.Token);

            var requestEntry = requestEntries.FirstOrDefault(x => x.SearchToken.Contains(movieTitle));
            requestEntry.Should().NotBeNull();
        }
    }
}