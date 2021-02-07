using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace SKIT.WebX.RESTful.WebApi
{
    internal class RequiredBindingMetadataProvider : IBindingMetadataProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void CreateBindingMetadata(BindingMetadataProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            /*
             * REF: https://www.cnblogs.com/tdfblog/p/required-and-bindrequired-in-asp-net-core-mvc.html
             */

            if (context.PropertyAttributes != null && context.PropertyAttributes.OfType<RequiredAttribute>().Any())
            {
                context.BindingMetadata.IsBindingRequired = true;
            }
        }
    }
}
