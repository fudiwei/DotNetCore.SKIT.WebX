using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace STEP.WebX
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers a configuration instance which TOptions will bind against.
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterOptions<TOptions>(this IServiceCollection services, IConfiguration configuration)
                where TOptions : class, IOptions<TOptions>, new()
        {
            const string SUFFIX = "Options";
            string sectionKey = typeof(TOptions).Name;
            if (sectionKey.EndsWith(SUFFIX))
            {
                sectionKey = sectionKey.Remove(sectionKey.Length - SUFFIX.Length);
            }

            return RegisterOptions<TOptions>(services, configuration, sectionKey);
        }

        /// <summary>
        /// Registers a configuration instance which TOptions will bind against with the specified key.
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="sectionKey"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterOptions<TOptions>(this IServiceCollection services, IConfiguration configuration, string sectionKey)
                where TOptions : class, IOptions<TOptions>, new()
        {
            services.AddOptions();
            return services.Configure<TOptions>(configuration.GetSection(sectionKey));
        }
    }
}
