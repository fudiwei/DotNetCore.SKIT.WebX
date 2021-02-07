using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BadRequest400InvalidQueryException : Api400BadRequestException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.BadRequest_InvalidQuery; } }

        /// <summary>
        /// 
        /// </summary>
        public string QueryName { get; }

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                if (string.IsNullOrEmpty(QueryName))
                    return base.Message;

                return $"invalid query \"{QueryName}\"";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public BadRequest400InvalidQueryException()
            : this(null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryName"></param>
        public BadRequest400InvalidQueryException(string queryName)
            : base()
        {
            QueryName = queryName;
        }
    }
}
