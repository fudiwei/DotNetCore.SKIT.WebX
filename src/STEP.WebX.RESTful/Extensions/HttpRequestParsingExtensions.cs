using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Microsoft.AspNetCore.Http
{
    using STEP.WebX.RESTful.Exceptions;

    /// <summary>
    /// 
    /// </summary>
    public static class HttpRequestParsingExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="action"></param>
        /// <param name="throwIfException"></param>
        /// <returns></returns>
        public static async Task ParseJsonAsync(this HttpRequest request, Action<JToken> action, bool throwIfException = true)
        {
            try
            {
                action.Invoke(await request.ReadBodyAsJsonAsync());
            }
            catch (RESTfulException ex) when (throwIfException)
            {
                if (throwIfException)
                    throw ex;
            }
            catch (Exception ex)
            {
                if (throwIfException)
                    throw new BadRequest400DataParsingException("An error occured when trying to parse request body to JSON.", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="func"></param>
        /// <param name="throwIfException"></param>
        /// <returns></returns>
        public static async Task<T> ParseJsonAsync<T>(this HttpRequest request, Func<JToken, T> func, bool throwIfException = true)
        {
            try
            {
                return func.Invoke(await request.ReadBodyAsJsonAsync());
            }
            catch (RESTfulException ex)
            {
                if (throwIfException)
                    throw ex;
                else
                    return default(T);
            }
            catch (Exception ex)
            {
                if (throwIfException)
                    throw new BadRequest400DataParsingException("An error occured when trying to parse request body to JSON.", ex);
                else
                    return default(T);
            }
        }
    }
}