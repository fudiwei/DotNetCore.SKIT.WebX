using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RequestTimeout408TimeoutOrExpiredException : Api408TimeoutException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.RequestTimeout_TimeoutOrExpired; } }

        /// <summary>
        /// 
        /// </summary>
        public RequestTimeout408TimeoutOrExpiredException()
            : base()
        {
        }
    }
}
