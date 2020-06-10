using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace STEP.WebX.Extensions.RESTfulSecurity
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AntiExpiredRequestNonValidateAttribute : Attribute, IFilterMetadata
    {
    }
}