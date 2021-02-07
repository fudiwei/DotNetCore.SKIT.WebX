using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Api403ForbiddenException : RESTfulException
    {
        /// <summary>
        /// 
        /// </summary>
        protected Api403ForbiddenException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        protected Api403ForbiddenException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        protected Api403ForbiddenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
