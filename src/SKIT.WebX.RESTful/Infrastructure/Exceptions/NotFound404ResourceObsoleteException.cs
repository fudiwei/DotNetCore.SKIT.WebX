using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NotFound404ResourceObsoleteException : Api404NotFoundException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.NotFound_ResourceObsolete; } }

        /// <summary>
        /// 
        /// </summary>
        public string ResourceName { get; }

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                if (string.IsNullOrEmpty(ResourceName))
                    return base.Message;

                return $"an obsolete \"{ResourceName}\"";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public NotFound404ResourceObsoleteException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resName"></param>
        public NotFound404ResourceObsoleteException(string resName)
        {
            ResourceName = resName ?? string.Empty;
        }
    }
}
