using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class QuotaExceeded429DuplicateSignatureException : Api429QuotaExceededException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.QuotaExceeded_DuplicateSignature; } }

        /// <summary>
        /// 
        /// </summary>
        public QuotaExceeded429DuplicateSignatureException()
            : base()
        {
        }
    }
}
