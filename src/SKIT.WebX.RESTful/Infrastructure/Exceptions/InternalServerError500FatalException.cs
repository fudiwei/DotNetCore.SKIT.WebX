using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class InternalServerError500FatalException : Api500InternalServerErrorException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.InternalServerError_Fatal; } }

        /// <summary>
        /// 
        /// </summary>
        public InternalServerError500FatalException()
            : base()
        {
        }
    }
}
