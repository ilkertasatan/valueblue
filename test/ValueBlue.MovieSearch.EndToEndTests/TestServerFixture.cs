using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ValueBlue.MovieSearch.Api;

namespace ValueBlue.MovieSearch.EndToEndTests
{
    public class TestServerFixture
    {
        private readonly TestServer _testServer;
        private const string DefaultApiKey = "e8ee624b";

        public TestServerFixture()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseContentRoot("")
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build()
                )
                .UseStartup<Startup>();
            
            _testServer = new TestServer(webHostBuilder);
        }
        
        public async Task<TResponse> GetAsync<TResponse>(string uri, CancellationToken cancellationToken)
        {
            var httpClient = _testServer.CreateClient();
            httpClient.DefaultRequestHeaders.Add("x-api-key", DefaultApiKey);
            var httpResponse = await httpClient.GetAsync(uri, cancellationToken);
            
            return JsonConvert.DeserializeObject<TResponse>(await httpResponse.Content.ReadAsStringAsync());
        }
    }
}