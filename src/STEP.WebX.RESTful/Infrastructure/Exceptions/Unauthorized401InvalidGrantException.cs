using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Unauthorized401InvalidGrantException : Api401UnauthorizedException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.Unauthorized_InvalidGrant; } }

        /// <summary>
        /// 
        /// </summary>
        public Unauthorized401InvalidGrantException()
            : base()
        {
        }
    }
}
