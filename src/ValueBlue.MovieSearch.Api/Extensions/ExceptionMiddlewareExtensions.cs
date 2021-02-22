using System.Text;
using Flurl.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ValueBlue.MovieSearch.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(x =>
            {
                x.Run(async context =>
                {
                    var logger = context.RequestServices.GetService<ILogger>();
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;

                    logger.LogError(exception, "Error: {ErrorMessage}", exception.Message);
                    
                    var statusCode = StatusCodes.Status500InternalServerError;
                    object errorResult;

                    switch (exception)
                    {
                        case FlurlHttpTimeoutException timeoutException:
                            errorResult = new
                            {
                                status = StatusCodes.Status502BadGateway,
                                message = timeoutException.Message
                            };

                            statusCode = StatusCodes.Status502BadGateway;
                            break;
                        case FlurlHttpException httpException:
                            errorResult = new
                            {
                                status = httpException.StatusCode,
                                message = httpException.Message
                            };

                            statusCode = httpException.StatusCode.GetValueOrDefault();
                            break;
                        default:
                            errorResult = new ProblemDetails
                            {
                                Status = context.Response.StatusCode,
                                Title = "An error occurred"
                            };
                            break;
                    }

                    context.Response.StatusCode = statusCode;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResult), Encoding.UTF8);
                });
            });

            return app;
        }
    }
}