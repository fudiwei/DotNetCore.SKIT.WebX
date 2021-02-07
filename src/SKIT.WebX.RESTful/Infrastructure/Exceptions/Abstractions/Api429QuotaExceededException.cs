using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Api429QuotaExceededException : RESTfulException
    {
        /// <summary>
        /// 
        /// </summary>
        protected Api429QuotaExceededException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        protected Api429QuotaExceededException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        protected Api429QuotaExceededException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
