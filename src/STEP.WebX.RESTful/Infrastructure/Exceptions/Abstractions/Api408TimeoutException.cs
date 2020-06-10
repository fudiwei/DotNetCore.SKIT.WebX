using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Api408TimeoutException : RESTfulException
    {
        /// <summary>
        /// 
        /// </summary>
        protected Api408TimeoutException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        protected Api408TimeoutException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        protected Api408TimeoutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
