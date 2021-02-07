using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class Unauthorized401HotlinkingException : Api401UnauthorizedException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.Unauthorized_HotLinking; } }

        /// <summary>
        /// 
        /// </summary>
        public Unauthorized401HotlinkingException()
            : base()
        {
        }
    }
}
