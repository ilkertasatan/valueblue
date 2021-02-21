using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ValueBlue.MovieSearch.Api.Middlewares
{
    public class AuthorizationByApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public AuthorizationByApiKeyMiddleware(
            RequestDelegate next,
            IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.Contains("/request-entries"))
            {
                var apiKey = context.Request.Headers["x-api-key"];
                var omDbApiKey = _configuration["MovieService:OMDb:ApiKey"];

                if (apiKey != omDbApiKey)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }
            
            await _next(context);
        }
    }
}