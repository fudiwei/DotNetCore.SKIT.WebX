using System;
using Microsoft.AspNetCore.Builder;

namespace SKIT.WebX.RESTful
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationBuilderCorsExtensions
    {
        /// <summary>
        /// Adds a CORS middleware to the web application pipeline to allow cross domain requests.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDefaultCors(this IApplicationBuilder builder)
        {
            return builder.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }
    }
}