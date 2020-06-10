using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace STEP.WebX.RESTful
{
    using Exceptions;
    using WebApi;

    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public abstract class RESTfulControllerBase : Controller
    {
        /// <summary>
        /// Creates a Microsoft.AspNetCore.Mvc.JsonResult object that serializes the specified data object to JSON.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NonAction]
        public override JsonResult Json(object data)
        {
            return base.Json(data, JsonHelper.DefaultNewtonsoftJsonSerializerSettings);
        }

        /// <summary>
        /// Creates a <see cref="IRESTfulResult"/> object that serializes the specified data object to JSON with RESTful Web API response format.
        /// </summary>
        /// <param name="ret"></param>
        /// <returns></returns>
        [NonAction]
        public virtual IRESTfulResult RESTfulCommonData(bool ret)
        {
            return new RESTfulCommonDataResult(ret);
        }

        /// <summary>
        /// Creates a <see cref="IRESTfulResult"/> object that serializes the specified data object to JSON with RESTful Web API response format.
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [NonAction]
        public virtual IRESTfulResult RESTfulCommonData(bool ret, object data)
        {
            return new RESTfulCommonDataResult(ret, data);
        }

        /// <summary>
        /// Creates a <see cref="IRESTfulResult"/> object that serializes the specified data object to JSON with RESTful Web API paging response format.
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="items"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [NonAction]
        public virtual IRESTfulResult RESTfulPagingData(bool ret, IEnumerable<object> items, int? totalCount = null)
        {
            if (!totalCount.HasValue)
                return new RESTfulPagingDataResult(ret, items);

            return new RESTfulPagingDataResult(ret, items, totalCount.Value);
        }

        /// <summary>
        /// Creates a <see cref="IRESTfulResult"/> object that serializes the specified data object to JSON with RESTful Web API paging response format.
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="items"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [NonAction]
        public virtual IRESTfulResult RESTfulPagingData(bool ret, int page, int limit, IEnumerable<object> items, int? totalCount = null)
        {
            if (!totalCount.HasValue)
                return new RESTfulPagingDataResult(ret, page, limit, items);

            return new RESTfulPagingDataResult(ret, page, limit, items, totalCount.Value);
        }

        /// <summary>
        /// Creates a <see cref="IRESTfulResult"/> object that serializes the specified data object to JSON with RESTful Web API paging response format.
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="items"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [NonAction]
        public virtual IRESTfulResult RESTfulPagingData(bool ret, string offset, int limit, IEnumerable<object> items, int? totalCount = null)
        {
            if (!totalCount.HasValue)
                return new RESTfulPagingDataResult(ret, offset, limit, items);

            return new RESTfulPagingDataResult(ret, offset, limit, items, totalCount.Value);
        }

        /// <summary>
        /// Creates a <see cref="IRESTfulResult"/> object that serializes the specified data object to JSON with RESTful Web API paging response format.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        public virtual IRESTfulResult RESTfulPagingData(RESTfulPagingDataResult result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            return result;
        }

        /// <summary>
        /// Creates a <see cref="IRESTfulResult"/> object that serializes the specified data object to JSON with RESTful Web API message response format.
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="failure"></param>
        /// <returns></returns>
        [NonAction]
        public virtual IRESTfulResult RESTfulFailureData(bool ret, string failure)
        {
            return new RESTfulFailureDataResult(ret, failure);
        }

        /// <summary>
        /// Creates a <see cref="IRESTfulResult"/> object that serializes the specified data object to JSON with RESTful Web API message response format.
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="failure"></param>
        /// <param name="exts"></param>
        /// <returns></returns>
        [NonAction]
        public virtual IRESTfulResult RESTfulFailureData(bool ret, string failure, params KeyValuePair<string, object>[] exts)
        {
            return new RESTfulFailureDataResult(ret, failure, exts);
        }

        /// <summary>
        /// Creates a <see cref="IRESTfulResult"/> object that serializes the specified data object to JSON with RESTful Web API message response format.
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="failure"></param>
        /// <param name="exts"></param>
        /// <returns></returns>
        [NonAction]
        public virtual IRESTfulResult RESTfulFailureData(bool ret, string failure, params (string Key, object Value)[] exts)
        {
            return new RESTfulFailureDataResult(ret, failure, exts?.Select(e => new KeyValuePair<string, object>(e.Key, e.Value))?.ToArray());
        }

        /// <summary>
        /// Creates a <see cref="IRESTfulResult"/> object that serializes the specified data object to JSON with RESTful Web API message response format.
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="failure"></param>
        /// <param name="exts"></param>
        /// <returns></returns>
        [NonAction]
        public virtual IRESTfulResult RESTfulFailureData(bool ret, string failure, IEnumerable<KeyValuePair<string, object>> exts)
        {
            return new RESTfulFailureDataResult(ret, failure, exts?.ToArray());
        }

        /// <summary>
        /// Creates a <see cref="IRESTfulResult"/> object that serializes the specified data object to JSON with RESTful Web API error response format.
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="errCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [NonAction]
        public virtual IRESTfulResult RESTfulException(int httpStatusCode, int errCode, string errMsg)
        {
            if (httpStatusCode < 400)
                throw new ArgumentOutOfRangeException(nameof(httpStatusCode));

            return new RESTfulResult(httpStatusCode, errCode, errMsg);
        }

        /// <summary>
        /// Creates a <see cref="IRESTfulResult"/> object that serializes the specified data object to JSON with RESTful Web API error response format.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        [NonAction]
        public virtual IRESTfulResult RESTfulException(RESTfulException ex)
        {
            return new RESTfulExceptionResult(ex);
        }
    }
}
