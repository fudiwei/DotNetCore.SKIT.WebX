using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Unauthorized401NoAccessException : Api401UnauthorizedException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.Unauthorized_NoAccess; } }

        /// <summary>
        /// 
        /// </summary>
        public Unauthorized401NoAccessException()
            : base()
        {
        }
    }
}
