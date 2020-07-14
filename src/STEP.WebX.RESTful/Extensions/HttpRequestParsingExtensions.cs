using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace STEP.WebX.RESTful
{
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
            catch (Exceptions.RESTfulException ex)
            {
                if (throwIfException)
                    throw ex;
            }
            catch (Exception ex)
            {
                if (throwIfException)
                    throw new Exceptions.BadRequest400DataParsingException("An error occured when trying to parse request body to JSON.", ex);
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
            catch (Exceptions.RESTfulException ex)
            {
                if (throwIfException)
                    throw ex;
                else
                    return default(T);
            }
            catch (Exception ex)
            {
                if (throwIfException)
                    throw new Exceptions.BadRequest400DataParsingException("An error occured when trying to parse request body to JSON.", ex);
                else
                    return default(T);
            }
        }
    }
}