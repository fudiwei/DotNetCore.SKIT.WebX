using System;

namespace STEP.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Forbidden403PermissionRequiredException : Api403ForbiddenException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.Forbidden_RequirePermissions; } }

        /// <summary>
        /// 
        /// </summary>
        public Forbidden403PermissionRequiredException() 
            : base()
        {
        }
    }
}
