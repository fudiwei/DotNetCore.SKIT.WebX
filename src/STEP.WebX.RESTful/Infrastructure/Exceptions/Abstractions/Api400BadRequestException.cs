using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Api400BadRequestException : RESTfulException
    {
        /// <summary>
        /// 
        /// </summary>
        protected Api400BadRequestException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected Api400BadRequestException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        protected Api400BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
