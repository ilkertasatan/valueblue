﻿using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ValueBlue.MovieSearch.Api.Extensions
{
    public static class ApiControllerExtensions
    {
        public static IServiceCollection AddApiControllers(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(config =>
                {
                    config.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            return services;
        }
    }
}