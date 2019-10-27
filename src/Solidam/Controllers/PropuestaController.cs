using System;
using Helpers;
using Models;
using Services;
using System.Linq;
using System.Web.Mvc;
using Utils;
using Enums;
using Solidam.ViewModel;
using MotivoDenuncia = Models.MotivoDenuncia;
using System.Collections.Generic;

namespace Solidam.Controllers
{
    public class PropuestaController : BaseController
    {
        public ActionResult CrearPropuesta()
        {
            if(PropuestaService.TotalPropuestasActivas() == 3)
                return RedirectToAction("Inicio", "Inicio");

            List<SelectListItem> lst = new List<SelectListItem>();

            lst.Add(new SelectListItem() { Text = "Seleccionar", Value = "0", Disabled = true, Selected = true });
            lst.Add(new SelectListItem() { Text = "Monetaria", Value = "1" });
            lst.Add(new SelectListItem() { Text = "Insumos", Value = "2" });
            lst.Add(new SelectListItem() { Text = "Horas de trabajo", Value = "3" });

            ViewBag.Opciones = lst;

            return View();
        }

        [HttpPost]
        public ActionResult Crear(Propuestas p, System.Web.HttpPostedFileBase foto)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }

            string path = Server.MapPath("~/Images/Views/Propuesta/");

            p.Foto = System.IO.Path.GetFileName(foto.FileName);

            PropuestaService.AgregarPropuesta(p);

            foto.SaveAs(path + System.IO.Path.GetFileName(foto.FileName));

            return RedirectToAction("Inicio", "Inicio");
        }

        [HttpPost]
        public ActionResult Valorar(string mg, string nmg, PropuestasValoraciones pv)
        {
            PropuestasValoraciones propuestasValoraciones = new PropuestasValoraciones
            {
                IdPropuesta = pv.IdPropuesta,
                IdUsuario = SessionHelper.Usuario.IdUsuario,
                Valoracion = nmg == null
            };
            PropuestasValoracionesService.Crear(propuestasValoraciones);
            PropuestaService.PutPorcentajeAceptacion(pv.IdPropuesta);
            return RedirectToAction("Detalle", "Propuesta", new { id = pv.IdPropuesta });
        }

        public ActionResult Detalle(int id)
        {
            if (SessionHelper.Usuario == null)
            {
                TempData["pendingRoute"] = Url.Action("Detalle", "Propuesta", new { id = id });
                return RedirectToAction("Iniciar", "Seguridad");
            }
            var propuesta = PropuestaService.GetById(id);
            PropuestaDetalleViewModel propuestaDetalleViewModel = new PropuestaDetalleViewModel
            {
                Propuesta = propuesta,
                Denuncie = DenunciasService.Denuncie(id),
                Valoracion = PropuestasValoracionesService.Valore(id)
            };
            return View(propuestaDetalleViewModel);
        }

        [HttpGet]
        public ActionResult Denunciar(int id)
        {
            DenunciaViewModel viewModel = new DenunciaViewModel
            {
                IdPropuesta = id,
                MotivoDenuncia = MotivoDenunciaService.GetAll(),
                NombrePropuesta = PropuestaService.GetById(id).Nombre,
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Denunciar(Denuncias denuncia)
        {
            denuncia.FechaCreacion = DateTime.Now;
            denuncia.IdUsuario = SessionHelper.Usuario.IdUsuario;
            denuncia.Estado = (int)DenunciaEstado.EnRevision;
            DenunciasService.Crear(denuncia);
            return RedirectToAction("Inicio", "Inicio");
        }

        public ActionResult Donar(int id)
        {
            var propuesta = PropuestaService.GetById(id);
            DonarViewModel dvm = new DonarViewModel
            {
                Propuesta = propuesta,
                DonacionesMonetarias = PropuestaService.GetDonacionesMonetariasById(id),
                DonacionesHorasTrabajo = PropuestaService.GetDonacionesHorasTrabajoById(id),
                DonacionesInsumos = PropuestaService.GetDonacionesInsumosById(id)
            };
            return View(dvm);
        }
    }
}