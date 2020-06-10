using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BadRequest400LackOfParameterException : Api400BadRequestException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.BadRequest_LackOfParameter; } }

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

                return $"lack of parameter \"{ParameterName}\"";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public BadRequest400LackOfParameterException()
            : this(null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        public BadRequest400LackOfParameterException(string parameterName)
            : base()
        {
            ParameterName = parameterName;
        }
    }
}
