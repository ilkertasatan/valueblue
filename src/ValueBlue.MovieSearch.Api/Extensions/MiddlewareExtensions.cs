using Microsoft.AspNetCore.Builder;
using ValueBlue.MovieSearch.Api.Middlewares;

namespace ValueBlue.MovieSearch.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }

        public static IApplicationBuilder UseCustomAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationByApiKeyMiddleware>();
        }
    }
}