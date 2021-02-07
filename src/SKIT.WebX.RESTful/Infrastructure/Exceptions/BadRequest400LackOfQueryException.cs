using System;

namespace SKIT.WebX.RESTful.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BadRequest400LackOfQueryException : Api400BadRequestException
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Code { get { return ErrorCodes.BadRequest_LackOfQuery; } }

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

                return $"lack of query \"{QueryName}\"";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryName"></param>
        public BadRequest400LackOfQueryException(string queryName)
            : base()
        {
            QueryName = queryName;
        }
    }
}
