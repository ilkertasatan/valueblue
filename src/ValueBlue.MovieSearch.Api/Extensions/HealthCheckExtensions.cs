using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ValueBlue.MovieSearch.Api.HealthChecks;

namespace ValueBlue.MovieSearch.Api.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddApiHealthChecks(this IServiceCollection services)
        {
            services
                .AddHealthChecks()
                .AddCheck<LivenessHealthCheck>("Liveness", HealthStatus.Unhealthy)
                .AddMongoDb(
                    mongodbConnectionString: "",
                    name: "MongoDB",
                    failureStatus: HealthStatus.Unhealthy);

            return services;
        }
    }
}