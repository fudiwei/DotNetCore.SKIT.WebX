using System;

namespace STEP.WebX.RESTful.WebApi
{
    using Exceptions;

    /// <summary>
    /// Represents a exception result of RESTful Web API.
    /// </summary>
    public class RESTfulExceptionResult : RESTfulResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public RESTfulExceptionResult(RESTfulException ex)
        {
            if (ex is Api400BadRequestException)
                HttpStatusCode = 400;
            else if (ex is Api401UnauthorizedException)
                HttpStatusCode = 401;
            else if (ex is Api403ForbiddenException)
                HttpStatusCode = 403;
            else if (ex is Api404NotFoundException)
                HttpStatusCode = 404;
            else if (ex is Api405NotAllowedException)
                HttpStatusCode = 405;
            else if (ex is Api408TimeoutException)
                HttpStatusCode = 408;
            else if (ex is Api429QuotaExceededException)
                HttpStatusCode = 429;
            else if (ex is Api500InternalServerErrorException)
                HttpStatusCode = 500;
            else if (ex.Code >= 40000 && ex.Code <= 99999)
                HttpStatusCode = ex.Code / 100;
            else
                HttpStatusCode = 501;

            ErrorCode = ex.Code;
            ErrorMessage = ex.Message;
        }
    }
}
