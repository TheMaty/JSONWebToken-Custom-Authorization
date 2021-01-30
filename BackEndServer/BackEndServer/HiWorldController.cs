using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackEndServer
{
    [Authorize]
    [RoutePrefix("api/UR/SAM/Mobility/SupportWorker")]
    public class HiWorldController : ApiController
    {
        [Route("sayHi")]
        [HttpGet]
        public HttpResponseMessage SayHI()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Hello Authorized User");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}
