using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class Unauthorized401InvalidAccessKeyException : Api401UnauthorizedException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.Unauthorized_InvalidAccessKey; } }

        /// <summary>
        /// 
        /// </summary>
        public Unauthorized401InvalidAccessKeyException()
            : base()
        {
        }
    }
}
