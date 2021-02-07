using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace SKIT.WebX.Extensions.RESTfulSecurity
{
    /// <summary>
    /// 
    /// </summary>
    public class AntiHotlinkingMiddleware : IMiddleware
    {
        private readonly AntiHotlinkingOptions _options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configureOption"></param>
        public AntiHotlinkingMiddleware(Action<AntiHotlinkingOptions> configureOption)
        {
            _options = new AntiHotlinkingOptions();
            configureOption?.Invoke(_options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.ValidateHotlinking(_options.WhiteList);

            await next.Invoke(context);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class AntiHotlinkingMiddlewareExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAntiHotlinkingMiddleware(this IApplicationBuilder builder)
        {
            return UseAntiHotlinkingMiddleware(builder, (opt) => { });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configureOption"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAntiHotlinkingMiddleware(this IApplicationBuilder builder, Action<AntiHotlinkingOptions> configureOption)
        {
            return builder.UseMiddleware<AntiHotlinkingMiddleware>(configureOption);
        }
    }
}
