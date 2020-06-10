using System;
using Microsoft.AspNetCore.Mvc;

namespace STEP.WebX.RESTful.WebApi
{
    /// <summary>
    /// Defines a contract that represents the result of a RESTful Web API interface.
    /// </summary>
    public interface IRESTfulResult : IActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        int ErrorCode { get; }

        /// <summary>
        /// 
        /// </summary>
        string ErrorMessage { get; }
    }
}
