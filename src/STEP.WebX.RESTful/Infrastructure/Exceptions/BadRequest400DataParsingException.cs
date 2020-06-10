using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BadRequest400DataParsingException : Api400BadRequestException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.BadRequest_DataParsingError; } }

        /// <summary>
        /// 
        /// </summary>
        public BadRequest400DataParsingException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public BadRequest400DataParsingException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BadRequest400DataParsingException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
