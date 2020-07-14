using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace STEP.WebX.RESTful.Filters
{
    using Exceptions;

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    internal class ModelStateValidateFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.ModelState.MaxAllowedErrors = 1;
            if (!context.ModelState.IsValid)
            {
                var modelStateItem = context.ModelState.First();
                string modelStateKey = modelStateItem.Key;
                ModelStateEntry modelStateEntry = modelStateItem.Value;

                Exception exception = modelStateEntry.Errors.FirstOrDefault(err => err.Exception is RESTfulException)?.Exception;
                throw exception ?? new BadRequest400DataParsingException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            OnActionExecuting(context);
            
            await next.Invoke();
        }
    }
}

namespace STEP.WebX.RESTful
{
    using Filters;

    /// <summary>
    /// 
    /// </summary>
    public static class MvcOptionsModelStateValidatedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opts"></param>
        /// <returns></returns>
        public static MvcOptions ValidateModelState(this MvcOptions opts)
        {
            opts.Filters.Add(new ModelStateValidateFilterAttribute());
            return opts;
        }
    }
}