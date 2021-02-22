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
        private readonly ITranslateMovie<OmDbMovieResponse> _translator;

        public OmDbMovieService(
            Uri uri,
            string apiKey,
            ITranslateMovie<OmDbMovieResponse> translator)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey));

            _uri = uri;
            _apiKey = apiKey;
            _translator = translator;
        }

        public async Task<Movie> GetMovieByTitleAsync(string title)
        {
            var movie = Movie.Empty;

            try
            {
                var response = await _uri
                    .SetQueryParams(new
                    {
                        t = title,
                        apikey = _apiKey
                    })
                    .WithTimeout(TimeSpan.FromSeconds(3))
                    .GetJsonAsync<OmDbMovieResponse>();

                movie = _translator.Translate(response);
            }
            catch (FlurlHttpTimeoutException)
            {
            }
            catch (FlurlHttpException)
            {
            }

            return movie;
        }
    }
}