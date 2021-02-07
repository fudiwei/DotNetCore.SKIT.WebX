using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// Represents errors that occur during request handling.
    /// </summary>
    public abstract class RESTfulException : Exception
    {
        private readonly string _errmsg;

        /// <summary>
        /// 
        /// </summary>
        public abstract int Code { get; }

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                string msg = _errmsg;

                if (string.IsNullOrEmpty(msg))
                    msg = ErrorCodes.GetMessage(Code);

                if (string.IsNullOrEmpty(msg))
                    msg = base.Message;

                return msg;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected RESTfulException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        protected RESTfulException(string message) 
            : base(message)
        {
            _errmsg = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        protected RESTfulException(string message, Exception innerException)
            : base(message, innerException)
        {
            _errmsg = message;
        }
    }
}
