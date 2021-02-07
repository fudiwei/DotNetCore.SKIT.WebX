using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Api401UnauthorizedException : RESTfulException
    {
        /// <summary>
        /// 
        /// </summary>
        protected Api401UnauthorizedException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        protected Api401UnauthorizedException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        protected Api401UnauthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
