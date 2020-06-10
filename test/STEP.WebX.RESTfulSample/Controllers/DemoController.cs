using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace STEP.WebX.RESTfulSample.Controllers
{
    using RESTful;

    [Route("demo")]
    public class DemoController : RESTfulControllerBase
    {
        public DemoController()
        {
        }

        [Route("action-post")]
        [AcceptVerbs(RESTfulVerbs.Post)]
        public async Task<IActionResult> ActionPost([FromBody] Dictionary<string, string> requestMap)
        {
            return RESTfulCommonData(true, requestMap.Select(e => e.Value).ToList());
        }

        [Route("action-failure")]
        [AcceptVerbs(RESTfulVerbs.Post)]
        public async Task<IActionResult> ActionFailure()
        {
            return RESTfulFailureData(false, "failed", new KeyValuePair<string, object>("ext", null));
        }

        [Route("action-paging")]
        [AcceptVerbs(RESTfulVerbs.Post)]
        public async Task<IActionResult> ActionPaging()
        {
            return RESTfulFailureData(false, "failed", new KeyValuePair<string, object>("ext", null));
        }
    }
}
