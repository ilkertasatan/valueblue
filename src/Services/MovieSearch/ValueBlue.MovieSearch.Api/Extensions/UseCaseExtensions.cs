using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ValueBlue.MovieSearch.Api.UseCases.V1.GetRequestEntriesUsageReport;
using ValueBlue.MovieSearch.Api.UseCases.V1.SearchMovie;

namespace ValueBlue.MovieSearch.Api.Extensions
{
    public static class UseCaseExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSearchMovieUseCase(configuration)
                .AddGetRequestEntriesUsageReportUseCase();
                
            return services;
        }
    }
}