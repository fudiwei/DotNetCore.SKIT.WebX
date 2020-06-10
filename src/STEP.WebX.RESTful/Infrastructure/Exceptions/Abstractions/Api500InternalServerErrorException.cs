using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Api500InternalServerErrorException : RESTfulException
    {
        /// <summary>
        /// 
        /// </summary>
        protected Api500InternalServerErrorException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        protected Api500InternalServerErrorException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        protected Api500InternalServerErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
