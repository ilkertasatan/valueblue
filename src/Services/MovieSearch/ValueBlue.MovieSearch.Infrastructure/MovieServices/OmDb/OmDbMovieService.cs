using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.Movies;

namespace ValueBlue.MovieSearch.Infrastructure.MovieServices.OmDb
{
    public class OmDbMovieService :
        ISearchMovieByTitle
    {
        private readonly Uri _uri;
        private readonly string _apiKey;

        public OmDbMovieService(Uri uri, string apiKey)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey));

            _uri = uri;
            _apiKey = apiKey;
        }

        public async Task<Movie> GetMovieByTitleAsync(string title)
        {
            var movie = await _uri
                .SetQueryParams(new
                {
                    t = title,
                    apikey = _apiKey
                })
                .ConfigureRequest(s => { s.Timeout = TimeSpan.FromSeconds(3); })
                .GetJsonAsync<Movie>();

            return movie;
        }
    }
}