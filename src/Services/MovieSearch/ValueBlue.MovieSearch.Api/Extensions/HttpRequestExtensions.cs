using Microsoft.AspNetCore.Http;

namespace ValueBlue.MovieSearch.Api.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string IpAddress(this HttpRequest request)
        {
            return request == null ? 
                string.Empty :
                request.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}