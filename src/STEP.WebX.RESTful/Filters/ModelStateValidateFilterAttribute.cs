using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace STEP.WebX.RESTful.Filters
{
    using Exceptions;
    using WebApi;

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    internal class ModelStateValidateFilterAttribute : ActionFilterAttribute
    {
        private void ValidateModelState(FilterContext context)
        {
            context.ModelState.MaxAllowedErrors = 1;
            if (!context.ModelState.IsValid)
            {
                RESTfulException exception = context.ModelState
                    .SelectMany(entry => entry.Value.Errors)
                    .FirstOrDefault(err => err.Exception is RESTfulException)
                    ?.Exception as RESTfulException;
                if (exception == null)
                {
                    ModelStateEntry entry = context.ModelState.FirstOrDefault().Value;
                    ModelError error = entry.Errors.FirstOrDefault();
                    exception = new BadRequest400DataParsingException(error?.ErrorMessage, error?.Exception);
                }

                if (context is ActionExecutingContext actionExecutingContext)
                {
                    actionExecutingContext.Result = new RESTfulExceptionResult(exception);
                }
                else if (context is ResultExecutingContext resultExecutingContext)
                {
                    resultExecutingContext.Result = new RESTfulExceptionResult(exception);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.ActionArguments.Any())
            {
                ValidateModelState(context);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            ValidateModelState(context);
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
        /// <param name="options"></param>
        /// <returns></returns>
        public static MvcOptions ValidateModelState(this MvcOptions options)
        {
            options.Filters.Add(new ModelStateValidateFilterAttribute());
            return options;
        }
    }
}