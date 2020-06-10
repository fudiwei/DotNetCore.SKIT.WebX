using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TimeoutInvalidTimestampException : Api408TimeoutException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.RequestTimeout_InvalidTimestamp; } }

        /// <summary>
        /// 
        /// </summary>
        public TimeoutInvalidTimestampException()
            : base()
        {
        }
    }
}
