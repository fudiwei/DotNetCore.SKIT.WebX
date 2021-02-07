using System;

namespace SKIT.WebX.RESTful
{
    /// <summary>
    /// Uses a single status type to represent all kinds of errors. 
    /// A status value of OK represents no error and has an integer value of zero.
    /// All other status values represent an error.
    /// </summary>
    public static partial class ErrorCodes
    {
        /// <summary>
        /// -1: Unkown.
        /// </summary>
        public const int Unkown = -1;

        #region 20X: OK
        /// <summary>
        /// 0: OK.
        /// </summary>
        public const int OK = 0;
        #endregion

        #region 400: BadRequest
        private const int BADREQUEST = 40000;

        /// <summary>
        /// 40000: Data parsing error.
        /// </summary>
        public const int BadRequest_DataParsingError = BADREQUEST + 0;

        /// <summary>
        /// 40001: Lack of parameter.
        /// </summary>
        public const int BadRequest_LackOfParameter = BADREQUEST + 1;

        /// <summary>
        /// 40002: Invalid parameter.
        /// </summary>
        public const int BadRequest_InvalidParameter = BADREQUEST + 2;

        /// <summary>
        /// 40011: Lack of query condition.
        /// </summary>
        public const int BadRequest_LackOfQuery = BADREQUEST + 11;

        /// <summary>
        /// 40012: Invalid query condition.
        /// </summary>
        public const int BadRequest_InvalidQuery = BADREQUEST + 12;

        /// <summary>
        /// 40021: Lack of file.
        /// </summary>
        public const int BadRequest_LackOfFile = BADREQUEST + 21;

        /// <summary>
        /// 40022: Invalid format of file.
        /// </summary>
        public const int BadRequest_InvalidFile = BADREQUEST + 22;

        /// <summary>
        /// 40099: Payload is too large.
        /// </summary>
        public const int BadRequest_PayloadTooLarge = BADREQUEST + 99;
        #endregion

        #region 401: Unauthorized
        private const int UNAUTHORIZED = 40100;

        /// <summary>
        /// 40100: No access.
        /// </summary>
        public const int Unauthorized_NoAccess = UNAUTHORIZED + 0;

        /// <summary>
        /// 40101: Lack of grant.
        /// </summary>
        public const int Unauthorized_LackOfGrant = UNAUTHORIZED + 1;

        /// <summary>
        /// 40102: Invalid grant.
        /// </summary>
        public const int Unauthorized_InvalidGrant = UNAUTHORIZED + 2;

        /// <summary>
        /// 40103: Lack of request signature.
        /// </summary>
        public const int Unauthorized_LackOfSignature = UNAUTHORIZED + 3;

        /// <summary>
        /// 40104: Invalid request signature.
        /// </summary>
        public const int Unauthorized_InvalidSignature = UNAUTHORIZED + 4;

        /// <summary>
        /// 40105: Lack of AccessKey.
        /// </summary>
        public const int Unauthorized_LackOfAccessKey = UNAUTHORIZED + 5;

        /// <summary>
        /// 40106: Invalid AccessKey.
        /// </summary>
        public const int Unauthorized_InvalidAccessKey = UNAUTHORIZED + 6;

        /// <summary>
        /// 40110: Hot linking or the referrer is not in the whitelist.
        /// </summary>
        public const int Unauthorized_HotLinking = UNAUTHORIZED + 10;
        #endregion

        #region 403: Forbidden
        private const int FORBIDDEN = 40300;

        /// <summary>
        /// 40300: Permission is required.
        /// </summary>
        public const int Forbidden_RequirePermissions = FORBIDDEN + 0;
        #endregion

        #region 404: NotFound
        private const int NOTFOUND = 40400;

        /// <summary>
        /// 40400: Resource not exists.
        /// </summary>
        public const int NotFound_ResourceNotExists = NOTFOUND + 0;

        /// <summary>
        /// 40401: Resource was obsolete.
        /// </summary>
        public const int NotFound_ResourceObsolete = NOTFOUND + 1;
        #endregion

        #region 405: NotAllowed
        private const int NOTALLOWED = 40500;

        /// <summary>
        /// 40500: Invalid method or no route is matched.
        /// </summary>
        public const int NotAllowed_InvalidMethod = NOTALLOWED + 0;
        #endregion

        #region 408: RequestTimeout
        private const int REQUESTTIMEOUT = 40800;

        /// <summary>
        /// 40800: Request timeout.
        /// </summary>
        public const int RequestTimeout_TimeoutOrExpired = REQUESTTIMEOUT + 0;

        /// <summary>
        /// 40801: Lack of timestamp.
        /// </summary>
        public const int RequestTimeout_LackOfTimestamp = REQUESTTIMEOUT + 1;

        /// <summary>
        ///40802: Invalid timestamp.
        /// </summary>
        public const int RequestTimeout_InvalidTimestamp = REQUESTTIMEOUT + 2;
        #endregion

        #region 429: QuotaExceeded
        private const int QUOTAEXCEEDED = 42900;

        /// <summary>
        /// 42900: Replay attack.
        /// </summary>
        public const int QuotaExceeded_ReplayAttack = QUOTAEXCEEDED + 0;

        /// <summary>
        /// 42901: Request signature is duplicate.
        /// </summary>
        public const int QuotaExceeded_DuplicateSignature = QUOTAEXCEEDED + 1;
        #endregion

        #region 500: InternalServerError
        private const int INTERNALSERVERERROR = 50000;

        /// <summary>
        /// 50000: Fatal.
        /// </summary>
        public const int InternalServerError_Fatal = INTERNALSERVERERROR + 0;

        /// <summary>
        /// 50031: Failed to invoke remote method.
        /// </summary>
        public const int InternalServerError_RemoteInvokingFailed = INTERNALSERVERERROR + 31;
        #endregion
    }
}
