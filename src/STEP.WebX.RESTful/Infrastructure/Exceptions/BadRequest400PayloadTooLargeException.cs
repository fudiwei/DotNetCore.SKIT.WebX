using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BadRequest400PayloadTooLargeException : Api400BadRequestException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code => ErrorCodes.BadRequest_PayloadTooLarge;

        /// <summary>
        /// 
        /// </summary>
        public BadRequest400PayloadTooLargeException()
            : base()
        {
        }
    }
}
