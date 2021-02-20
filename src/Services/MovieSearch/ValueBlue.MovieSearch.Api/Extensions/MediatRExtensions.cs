using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ValueBlue.MovieSearch.Application.UseCases.SearchMovie;

namespace ValueBlue.MovieSearch.Api.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(MovieSearchQuery).Assembly);

            return services;
        }
    }
}