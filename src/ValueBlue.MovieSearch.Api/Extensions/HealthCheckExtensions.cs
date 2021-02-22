using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ValueBlue.MovieSearch.Api.HealthChecks;

namespace ValueBlue.MovieSearch.Api.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddApiHealthChecks(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddCheck<LivenessHealthCheck>("Liveness", HealthStatus.Unhealthy)
                .AddCheck<OmDbHealthCheck>("OmDbApi", HealthStatus.Unhealthy)
                .AddMongoDb(
                    mongodbConnectionString: configuration["DbContext:MongoDb:ConnectionString"],
                    name: "MongoDB",
                    failureStatus: HealthStatus.Unhealthy);

            return services;
        }
    }
}