using Microsoft.Extensions.DependencyInjection;

namespace ValueBlue.MovieSearch.Api.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddLivenessHealthChecks(this IServiceCollection services)
        {
            return services;
        }    
    }
}