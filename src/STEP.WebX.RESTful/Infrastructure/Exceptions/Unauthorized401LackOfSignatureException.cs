using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Unauthorized401LackOfSignatureException : Api401UnauthorizedException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.Unauthorized_LackOfSignature; } }

        /// <summary>
        /// 
        /// </summary>
        public Unauthorized401LackOfSignatureException()
            : base()
        {
        }
    }
}
