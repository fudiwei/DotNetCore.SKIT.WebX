using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class InternalServerError500RemoteInvokingFailedException : Api500InternalServerErrorException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.InternalServerError_RemoteInvokingFailed; } }

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                if (string.IsNullOrEmpty(RemoteMethod))
                    return base.Message;

                if (string.IsNullOrEmpty(RemoteErrorMessage))
                    return $"remote invoke \"{RemoteMethod}\" failed";

                return $"remote invoke \"{RemoteMethod}\" failed (errmsg: {RemoteErrorMessage})";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RemoteMethod { get; }

        /// <summary>
        /// 
        /// </summary>
        public string RemoteErrorMessage { get; }

        /// <summary>
        /// 
        /// </summary>
        public InternalServerError500RemoteInvokingFailedException()
            : this(null, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteMethod"></param>
        public InternalServerError500RemoteInvokingFailedException(string remoteMethod)
            : this(remoteMethod, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteMethod"></param>
        /// <param name="removeErrMsg"></param>
        public InternalServerError500RemoteInvokingFailedException(string remoteMethod, string removeErrMsg)
            : base()
        {
            RemoteMethod = remoteMethod;
            RemoteErrorMessage = removeErrMsg;
        }
    }
}
