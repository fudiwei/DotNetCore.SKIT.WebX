using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace System.ComponentModel.DataAnnotations
{
    using STEP.WebX.RESTful.Exceptions;

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

            try
            {
                PropertyInfo propertyInfo = validationContext.ObjectInstance.GetType().GetProperty(validationContext.MemberName);
                JsonPropertyAttribute jsonProperty = propertyInfo?.GetCustomAttribute<JsonPropertyAttribute>();
                propertyName = jsonProperty?.PropertyName;
            }
            catch (ArgumentNullException) { }

            return propertyName ?? validationContext.MemberName ?? validationContext.DisplayName;
        }

        internal static bool IsMemberFromQuery(this ValidationContext validationContext)
        {
            /*
             * NOTICE: inaccurate result by this way.
             */

            HttpContext httpContext = validationContext.GetRequiredService<IHttpContextAccessor>().HttpContext;
            return HttpMethods.Get.Equals(httpContext.Request.Method, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
