using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class QuotaExceeded429ReplayAttackException : Api429QuotaExceededException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.QuotaExceeded_ReplayAttack; } }

        /// <summary>
        /// 
        /// </summary>
        public QuotaExceeded429ReplayAttackException()
            : base()
        {
        }
    }
}
