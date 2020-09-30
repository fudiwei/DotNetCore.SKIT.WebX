using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace STEP.WebX.RESTful
{
    /// <summary>
    /// 
    /// </summary>
    public static class ValidationContextPropertyExtensions
    {
        /// <summary>
        /// Gets or the actual name of the member to validate.
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public static string GetActualMemberName(this ValidationContext validationContext)
        {
            string propertyName = null;
            var propertyInfo = validationContext.ObjectInstance.GetType().GetProperty(validationContext.MemberName);

            if (propertyInfo != null)
            {
                var jsonPropertyAttribute = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
                if (jsonPropertyAttribute == null)
                {
#if !NETCORE_2_X
                var jsonPropertyNameAttribute = propertyInfo.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
                propertyName = jsonPropertyNameAttribute?.Name;
#endif
                }
                else
                {
                    propertyName = jsonPropertyAttribute.PropertyName;
                }
            }

            return propertyName ?? validationContext.MemberName ?? validationContext.DisplayName;
        }

        internal static bool IsMemberFromQuery(this ValidationContext validationContext)
        {
            var httpContext = validationContext.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            if (httpContext != null)
            {
                if (HttpMethods.Get.Equals(httpContext.Request.Method, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            var propertyInfo = validationContext.ObjectInstance.GetType().GetProperty(validationContext.MemberName);
            if (propertyInfo != null)
            {
                var fromQueryAttribute = propertyInfo.GetCustomAttributes<FromQueryAttribute>();
                return fromQueryAttribute != null;
            }

            return false;
        }
    }
}
