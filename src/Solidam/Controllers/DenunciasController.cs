using System;
using System.Linq;
using System.Web.Mvc;
using Helpers;
using Models;
using Services;

namespace Solidam.Controllers
{
    public class DenunciasController : BaseController
    {
        // GET: Denuncias
        [Route("denuncias")]
        public ActionResult Gestion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetDenuncias()
        {
            var draw = (Request.Form.GetValues("draw") ?? throw new InvalidOperationException()).FirstOrDefault();
            var start = (Request.Form.GetValues("start") ?? throw new InvalidOperationException()).FirstOrDefault();
            var length = (Request.Form.GetValues("length") ?? throw new InvalidOperationException()).FirstOrDefault();
            var sortColumn = (Request.Form.GetValues("columns[" + (Request.Form.GetValues("order[0][column]") ?? throw new InvalidOperationException()).FirstOrDefault() + "][name]") ?? throw new InvalidOperationException()).FirstOrDefault();
            var sortColumnDir = (Request.Form.GetValues("order[0][dir]") ?? throw new InvalidOperationException()).FirstOrDefault();
            var searchValue = (Request.Form.GetValues("search[value]") ?? throw new InvalidOperationException()).FirstOrDefault();

            //Paging Size (10,20,50,100)    
            var pageSize = length != null ? Convert.ToUInt32(length) : default;
            var skip = start != null ? Convert.ToUInt32(start) : default;
            var data = DenunciasService.Instance.GetAllDenunciasDTOByFilter(new Denuncias
            {
                MotivoDenuncia = new MotivoDenuncia
                {
                    Descripcion = searchValue
                },
                Comentarios = searchValue
            }, sortColumn, sortColumnDir, new Pager(skip, pageSize));

            //total number of rows count     
            var recordsFiltered = data.Count;
            var recordsTotal = DenunciasService.Instance.Get(null).Count;
            return Json(new {draw, recordsFiltered, recordsTotal, data });
        }

        [HttpPost]
        public JsonResult ModificarEstadoDenuncia(Denuncias denuncia)
        {
            DenunciasService.Instance.Put(denuncia);
            return Json(string.Empty);
        }
    }
}