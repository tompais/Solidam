using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;
namespace Solidam.Controllers
{
    public class DonacionesApiController : ApiController
    {
        [System.Web.Http.HttpGet]
        public JsonResult<string> Get()
        {
            return Json("Hola");
        }
    }
}
