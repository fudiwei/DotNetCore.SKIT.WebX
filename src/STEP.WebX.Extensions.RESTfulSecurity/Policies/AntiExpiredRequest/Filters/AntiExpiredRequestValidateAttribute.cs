using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace STEP.WebX.Extensions.RESTfulSecurity
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AntiExpiredRequestValidateAttribute : Attribute, IFilterMetadata, IAuthorizationFilter, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public int ExpirationLimit { get; set; } = AntiExpiredRequestOptions.DEFAULT_EXPIRATION_LIMIT;

        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!Enabled) return;
            if (context.Filters.Any(f => f is AntiExpiredRequestNonValidateAttribute)) return;
            if (context.Filters.LastOrDefault(f => f is AntiExpiredRequestValidateAttribute) != this) return; // 可能存在多个过滤器，以最后一个的设置为准

            context.HttpContext.ValidateRequestTimestamp(ExpirationLimit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await Task.Run(() => OnAuthorization(context), context.HttpContext.RequestAborted);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class MvcBuilderTimestampRequiredExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IMvcBuilder RequireTimestamp(this IMvcBuilder builder, Action<AntiExpiredRequestOptions> configure)
        {
            AntiExpiredRequestOptions configureOptions = new AntiExpiredRequestOptions();
            configure?.Invoke(configureOptions);
            builder.AddMvcOptions(options => options.Filters.Add(new AntiExpiredRequestValidateAttribute() { Enabled = true, ExpirationLimit = configureOptions.ExpirationLimit }));
            return builder;
        }
    }
}
