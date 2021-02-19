using Microsoft.Extensions.DependencyInjection;

namespace ValueBlue.MovieSearch.Api.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            return services;
        }
    }
}