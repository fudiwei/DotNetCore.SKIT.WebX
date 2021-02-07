using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Unauthorized401InvalidSignatureException : Api401UnauthorizedException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.Unauthorized_InvalidSignature; } }

        /// <summary>
        /// 
        /// </summary>
        public Unauthorized401InvalidSignatureException()
            : base()
        {
        }
    }
}
