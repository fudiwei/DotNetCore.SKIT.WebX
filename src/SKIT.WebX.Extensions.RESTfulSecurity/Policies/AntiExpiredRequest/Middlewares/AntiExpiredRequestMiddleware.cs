using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace SKIT.WebX.Extensions.RESTfulSecurity
{
    /// <summary>
    /// 
    /// </summary>
    public class AntiExpiredRequestMiddleware : IMiddleware
    {
        private readonly AntiExpiredRequestOptions _options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configureOption"></param>
        public AntiExpiredRequestMiddleware(Action<AntiExpiredRequestOptions> configureOption)
        {
            _options = new AntiExpiredRequestOptions();
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
            bool norequired = _options.Filter?.Invoke(context) ?? false;
            if (!norequired)
            {
                context.ValidateRequestTimestamp(_options.ExpirationLimit);
            }

            await next.Invoke(context);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class AntiExpiredRequestMiddlewareExtensions
    {
        /// <summary>
        /// 在当前请求管道中启用请求时间戳验证。
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAntiExpiredRequestMiddleware(this IApplicationBuilder builder)
        {
            return UseAntiExpiredRequestMiddleware(builder, (opt) => { });
        }

        /// <summary>
        /// 在当前请求管道中启用请求时间戳验证。
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configureOption"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAntiExpiredRequestMiddleware(this IApplicationBuilder builder, Action<AntiExpiredRequestOptions> configureOption)
        {
            return builder.UseMiddleware<AntiExpiredRequestMiddleware>(configureOption);
        }
    }
}
