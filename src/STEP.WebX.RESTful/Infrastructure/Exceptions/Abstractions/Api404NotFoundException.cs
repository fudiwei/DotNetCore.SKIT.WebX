using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Api404NotFoundException : RESTfulException
    {
        /// <summary>
        /// 
        /// </summary>
        protected Api404NotFoundException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        protected Api404NotFoundException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        protected Api404NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
