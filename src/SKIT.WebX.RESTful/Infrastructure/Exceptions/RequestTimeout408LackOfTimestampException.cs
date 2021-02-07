using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RequestTimeout408LackOfTimestampException : Api408TimeoutException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.RequestTimeout_LackOfTimestamp; } }

        /// <summary>
        /// 
        /// </summary>
        public RequestTimeout408LackOfTimestampException()
            : base()
        {
        }
    }
}
