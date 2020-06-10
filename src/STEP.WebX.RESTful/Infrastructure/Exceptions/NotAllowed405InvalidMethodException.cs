using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NotAllowed405InvalidMethodException : Api405NotAllowedException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.NotAllowed_InvalidMethod; } }

        /// <summary>
        /// 
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                if (string.IsNullOrEmpty(Method) || string.IsNullOrEmpty(Path))
                    return base.Message;

                return $"no route matches \"[{Method.ToUpper()}] {Path}\"";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="path"></param>
        public NotAllowed405InvalidMethodException(string method, string path)
            : base()
        {
            Method = method;
            Path = path;
        }
    }
}
