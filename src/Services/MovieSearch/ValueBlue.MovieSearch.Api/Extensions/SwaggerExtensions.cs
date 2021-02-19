using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ValueBlue.MovieSearch.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var apiVersionDescription in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(apiVersionDescription.GroupName,
                        new OpenApiInfo
                        {
                            Title = "ValueBlue.MovieSearch.Api",
                            Version = apiVersionDescription.ApiVersion.ToString(),
                            Description = "This is Movie Search Api to manage listing the movies from the external resources"
                        });
                }
            });
            services.AddSwaggerGenNewtonsoftSupport();
            
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ValueBlue.MovieSearch.Api V1"));

            return app;
        }
    }
}