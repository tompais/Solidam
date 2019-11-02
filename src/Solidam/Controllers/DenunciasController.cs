using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;

namespace Solidam.Controllers
{
    public class DenunciasController : BaseController
    {
        // GET: Denuncias
        [Route("denuncias")]
        public ActionResult Gestion()
        {
            return View(DenunciasService.Instance.Get(null));
        }
    }
}