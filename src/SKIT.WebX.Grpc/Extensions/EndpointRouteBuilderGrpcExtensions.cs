using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace SKIT.WebX.Grpc
{
    /// <summary>
    /// 
    /// </summary>
    public static class EndpointRouteBuilderGrpcExtensions
    {
        private static void AddIfNotContains<T>(this ICollection<T> collection, T item)
        {
            if (!collection.Contains(item))
            {
                collection.Add(item);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoints"></param>
        /// <returns></returns>
        public static void MapGrpcServices(this IEndpointRouteBuilder endpoints)
        {
            IList<Assembly> assemblies = new List<Assembly>();
            assemblies.AddIfNotContains(Assembly.GetExecutingAssembly());
            assemblies.AddIfNotContains(Assembly.GetCallingAssembly());
            assemblies.AddIfNotContains(Assembly.GetEntryAssembly());

            Type invokeType = typeof(GrpcEndpointRouteBuilderExtensions);
            foreach (Type type in assemblies.SelectMany(e => e.GetTypes()))
            {
                if (!type.IsAbstract && type.IsClass && type.GetCustomAttribute<GrpcServiceAttribute>(true) != null)
                {
                    invokeType
                        .GetMethod(nameof(GrpcEndpointRouteBuilderExtensions.MapGrpcService))
                        .MakeGenericMethod(type)
                        .Invoke(null, new[] { endpoints });
                }
            }
        }
    }
}
