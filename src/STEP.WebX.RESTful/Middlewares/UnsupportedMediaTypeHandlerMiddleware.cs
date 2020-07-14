using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace STEP.WebX.RESTful.Middlewares
{
    using Exceptions;

    /// <summary>
    /// 
    /// </summary>
    internal class UnsupportedMediaTypeHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public UnsupportedMediaTypeHandlerMiddleware(RequestDelegate next)
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
                if (context.Response.StatusCode == StatusCodes.Status415UnsupportedMediaType)
                {
                    throw new BadRequest400DataParsingException();
                }
            }
        }
    }
}

namespace STEP.WebX.RESTful
{
    using Middlewares;

    /// <summary>
    /// 
    /// </summary>
    public static class UnsupportedMediaTypeHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Convert status code from 415 to 400 if there is an unsupported media type error.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUnsupportedMediaTypeHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UnsupportedMediaTypeHandlerMiddleware>();
        }
    }
}