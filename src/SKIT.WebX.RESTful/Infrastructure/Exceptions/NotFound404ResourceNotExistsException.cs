using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NotFound404ResourceNotExistsException : Api404NotFoundException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.NotFound_ResourceNotExists; } }

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

                return $"a non-existent \"{ResourceName}\"";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public NotFound404ResourceNotExistsException()
            : this(null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resName"></param>
        public NotFound404ResourceNotExistsException(string resName)
        {
            ResourceName = resName;
        }
    }
}
