using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Solidam.Controllers
{
    public class HomeController : Controller
    {
        // GET: Error
        [Route("home/error")]
        public ActionResult Error(HttpStatusCode codigo = 0)
        {
            switch (codigo)
            {
                case HttpStatusCode.InternalServerError:
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Esto es muy vergonzoso, esperemos que no vuelva a pasar ..";
                    break;

                case HttpStatusCode.NotFound:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "La URL que está intentando ingresar no existe";
                    break;

                default:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "Algo salio muy mal :( ..";
                    break;
            }
            return View();
        }
    }
}