using System;

namespace SKIT.WebX.RESTful
{
    partial class ErrorCodes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errcode"></param>
        /// <returns></returns>
        public static string GetMessage(int errcode)
        {
            switch (errcode)
            {
                case OK:
                    return "ok";

                case BadRequest_DataParsingError:
                    return "data parsing error";
                case BadRequest_LackOfParameter:
                    return "lack of parameter";
                case BadRequest_InvalidParameter:
                    return "invalid parameter";
                case BadRequest_LackOfQuery:
                    return "lack of query";
                case BadRequest_InvalidQuery:
                    return "invalid query";
                case BadRequest_LackOfFile:
                    return "lack of file";
                case BadRequest_InvalidFile:
                    return "invalid file";
                case BadRequest_PayloadTooLarge:
                    return "payload too large";

                case Unauthorized_NoAccess:
                    return "no access";
                case Unauthorized_LackOfGrant:
                    return "lack of grant";
                case Unauthorized_InvalidGrant:
                    return "invalid grant";
                case Unauthorized_LackOfSignature:
                    return "lack of request signature";
                case Unauthorized_InvalidSignature:
                    return "invalid request signature";
                case Unauthorized_LackOfAccessKey:
                    return "lack of access key";
                case Unauthorized_InvalidAccessKey:
                    return "invalid access key";
                case Unauthorized_HotLinking:
                    return "hotlinking or referrer not in whitelist";

                case Forbidden_RequirePermissions:
                    return "permission required";

                case NotFound_ResourceNotExists:
                    return "non-exists resourse";
                case NotFound_ResourceObsolete:
                    return "obsolete resourse";

                case NotAllowed_InvalidMethod:
                    return "invalid method or no route is matched";

                case RequestTimeout_TimeoutOrExpired:
                    return "timeout or timestamp expired";
                case RequestTimeout_LackOfTimestamp:
                    return "lack of timestamp";
                case RequestTimeout_InvalidTimestamp:
                    return "invalid timestamp";

                case QuotaExceeded_ReplayAttack:
                    return "suspected replay attack";
                case QuotaExceeded_DuplicateSignature:
                    return "request signature is duplicate";

                case InternalServerError_Fatal:
                    return "fatal error";

                case InternalServerError_RemoteInvokingFailed:
                    return "failed to invoke remote method";

                default:
                    return string.Empty;
            }
        }
    }
}
