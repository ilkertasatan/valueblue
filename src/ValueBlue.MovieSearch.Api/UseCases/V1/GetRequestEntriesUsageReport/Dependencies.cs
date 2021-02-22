using Microsoft.Extensions.DependencyInjection;
using ValueBlue.MovieSearch.Domain.RequestEntries;
using ValueBlue.MovieSearch.Infrastructure.DataAccess.Repositories;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.GetRequestEntriesUsageReport
{
    public static class Dependencies
    {
        public static IServiceCollection AddGetRequestEntriesUsageReportUseCase(this IServiceCollection services)
        {
            services.AddScoped<IUsageReportRepository, UsageReportRepository>();

            return services;
        }
    }
}