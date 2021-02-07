using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Unauthorized401LackOfGrantException : Api401UnauthorizedException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.Unauthorized_LackOfGrant; } }

        /// <summary>
        /// 
        /// </summary>
        public Unauthorized401LackOfGrantException()
            : base()
        {
        }
    }
}
