using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BadRequest400InvalidParameterException : Api400BadRequestException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.BadRequest_InvalidParameter; } }

        /// <summary>
        /// 
        /// </summary>
        public string ParameterName { get; }

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                if (string.IsNullOrEmpty(ParameterName))
                    return base.Message;

                return $"invalid parameter \"{ParameterName}\"";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public BadRequest400InvalidParameterException()
            : this(null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        public BadRequest400InvalidParameterException(string parameterName)
        {
            ParameterName = parameterName;
        }
    }
}
