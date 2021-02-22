using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ValueBlue.MovieSearch.Application.Common.Behaviours;
using ValueBlue.MovieSearch.Application.UseCases.SearchMovie;

namespace ValueBlue.MovieSearch.Api.Extensions
{
    public static partial class MediatRExtensions
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(SearchMovieQuery).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));
            
            return services;
        }
    }
}