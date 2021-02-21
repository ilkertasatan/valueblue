using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Infrastructure.MovieServices.OmDb;

namespace ValueBlue.MovieSearch.Api.UseCases.V1.SearchMovie
{
    public static class Dependencies
    {
        public static IServiceCollection AddSearchMovieUseCase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISearchMovieByTitle>(provider =>
            {
                var uri = new Uri(configuration["MovieService:OMDb:ApiUrl"]);
                var apiKey = configuration["MovieService:OMDb:ApiKey"];

                return new OmDbMovieService(uri, apiKey);
            });

            return services;
        }
    }
}