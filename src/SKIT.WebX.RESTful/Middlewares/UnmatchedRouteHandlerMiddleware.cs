using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace SKIT.WebX.RESTful.Middlewares
{
    using Exceptions;

    /// <summary>
    /// 
    /// </summary>
    internal class UnmatchedRouteHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public UnmatchedRouteHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            await _next.Invoke(context);

            if (!context.Response.HasStarted)
            {
                if (context.Response.StatusCode == StatusCodes.Status404NotFound ||
                    context.Response.StatusCode == StatusCodes.Status405MethodNotAllowed)
                {
                    throw new NotAllowed405InvalidMethodException(context.Request.Method, context.Request.Path);
                }
            }
        }
    }
}

namespace SKIT.WebX.RESTful
{
    using Middlewares;

    /// <summary>
    /// 
    /// </summary>
    public static class NoRouteMatchedHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Convert status code from 404 to 405 if there is an no-matched route error.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUnmatchedRouteHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UnmatchedRouteHandlerMiddleware>();
        }
    }
}