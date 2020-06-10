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
    public class AntiHotlinkingValidateAttribute : Attribute, IFilterMetadata, IAuthorizationFilter, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public string[] WhiteList { get; set; }

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
            if (context.Filters.Any(f => f is AntiHotlinkingNonValidateAttribute)) return;
            if (context.Filters.Where((f) => f is AntiHotlinkingValidateAttribute).LastOrDefault() != this) return; // 可能存在多个过滤器，以最后一个的设置为准

            context.HttpContext.ValidateHotlinking(WhiteList);
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
    public static class MvcBuilderAntiHotlinkingExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IMvcBuilder AntiHotlinking(this IMvcBuilder builder, Action<AntiHotlinkingOptions> configure)
        {
            AntiHotlinkingOptions configureOptions = new AntiHotlinkingOptions();
            configure?.Invoke(configureOptions);
            builder.AddMvcOptions(options => options.Filters.Add(new AntiHotlinkingValidateAttribute() { Enabled = true, WhiteList = configureOptions.WhiteList }));
            return builder;
        }
    }
}
