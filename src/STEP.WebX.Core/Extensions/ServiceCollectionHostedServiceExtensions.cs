using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using STEP.WebX;

    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionHostedServiceExtensions
    {
        private static void AddIfNotContains<T>(this ICollection<T> collection, T item)
        {
            if (!collection.Contains(item))
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Auto register all the members which implement <see cref="IHostedService" />.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            IList<Assembly> assemblies = new List<Assembly>();
            assemblies.AddIfNotContains(Assembly.GetExecutingAssembly());
            assemblies.AddIfNotContains(Assembly.GetCallingAssembly());
            assemblies.AddIfNotContains(Assembly.GetEntryAssembly());

            Type baseType = typeof(IHostedService);
            foreach (Type type in assemblies.SelectMany(e => e.GetTypes()))
            {
                if (baseType.IsAssignableFrom(type) && !type.IsAbstract && type.IsClass)
                {
                    services.AddSingleton(baseType, type);
                }
            }

            return services;
        }
    }
}
