using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BadRequest400LackOfFileException : Api400BadRequestException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.BadRequest_LackOfFile; } }

        /// <summary>
        /// 
        /// </summary>
        public string FileKey { get; }

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                if (string.IsNullOrEmpty(FileKey))
                    return base.Message;

                return $"lack of file \"{FileKey}\"";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public BadRequest400LackOfFileException()
            : this(null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileKey"></param>
        public BadRequest400LackOfFileException(string fileKey)
            : base()
        {
            FileKey = fileKey;
        }
    }
}
