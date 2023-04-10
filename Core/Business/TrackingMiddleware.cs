using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;

namespace BlitzerCore.Business
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDbContext mDbContext;
        public TrackingMiddleware(RequestDelegate next, IDbContext aContext)
        {
            _next = next;
            mDbContext = aContext;
        }

        public System.Threading.Tasks.Task Invoke(HttpContext httpContext)
        {
            //var IPAddr = httpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            //new DAPattern<UserTracking>().Save(new UserTracking() { IpAddress = IPAddr, Path = httpContext.Request.Path }, 0, (RepositoryContext)mDbContext);
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TrackingMiddlewareExtensions
    {
        public static IApplicationBuilder UseTrackingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TrackingMiddleware>();
        }
    }
}
