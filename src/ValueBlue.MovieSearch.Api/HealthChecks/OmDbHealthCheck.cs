using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ValueBlue.MovieSearch.Api.HealthChecks
{
    public class OmDbHealthCheck :
        IHealthCheck
    {
        private readonly IConfiguration _configuration;

        public OmDbHealthCheck(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var uri = new Uri(_configuration["MovieService:OMDb:ApiUrl"]);
                var apiKey = _configuration["MovieService:OMDb:ApiKey"];
                
                await $"{uri}?t=Batman&apikey={apiKey}"
                    .GetJsonAsync(cancellationToken: cancellationToken);
                
                return HealthCheckResult.Healthy();
            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}