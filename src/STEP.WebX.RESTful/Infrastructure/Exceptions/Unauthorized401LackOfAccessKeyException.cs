using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Unauthorized401LackOfAccessKeyException : Api401UnauthorizedException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.Unauthorized_LackOfAccessKey; } }

        /// <summary>
        /// 
        /// </summary>
        public Unauthorized401LackOfAccessKeyException()
            : base()
        {
        }
    }
}
