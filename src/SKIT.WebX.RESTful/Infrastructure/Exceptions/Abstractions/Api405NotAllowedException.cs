using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Api405NotAllowedException : RESTfulException
    {
        /// <summary>
        /// 
        /// </summary>
        protected Api405NotAllowedException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        protected Api405NotAllowedException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        protected Api405NotAllowedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
