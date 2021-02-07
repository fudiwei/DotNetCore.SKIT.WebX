using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BadRequest400InvalidFileException : Api400BadRequestException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.BadRequest_InvalidFile; } }

        /// <summary>
        /// 
        /// </summary>
        public BadRequest400InvalidFileException()
            : base()
        {
        }
    }
}
