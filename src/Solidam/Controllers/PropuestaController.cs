using System;
using Helpers;
using Models;
using Services;
using System.Linq;
using System.Web.Mvc;
using Utils;
using Enums;
using NUnit.Framework;
using Solidam.ViewModel;
using MotivoDenuncia = Models.MotivoDenuncia;

namespace Solidam.Controllers
{
    public class PropuestaController : BaseController
    {
        public ActionResult CrearPropuesta()
        {
            return View();
        }

        public ActionResult Buscar(string nombre)
        {
            ViewBag.palabra = nombre;

            var propuestaBuscadas =  PropuestaService.ObtenerPropuestasPorNombreYUsuario(nombre);

            return View("PropuestasBuscadas", propuestaBuscadas);
        }

        public ActionResult MiPropuestas()
        {

            var misPropuestas = PropuestaService.ObtenerPropuestasUsuario(SessionHelper.Usuario.IdUsuario);

            return View("MisPropuestas", misPropuestas);
        }

        [HttpPost]
        public ActionResult Crear(Propuestas p, System.Web.HttpPostedFileBase foto)
        {
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
            var motivos = MotivoDenunciaService.GetAll().Select(m => new SelectListItem
            {
                Text = m.Descripcion,
                Value = m.IdMotivoDenuncia.ToString(),
            }).ToList();
            motivos = motivos.Prepend(new SelectListItem
            {
                Value = "0",
                Text = "Seleccione un motivo",
                Disabled = true,
                Selected = true
            }).ToList();
            DenunciaViewModel viewModel = new DenunciaViewModel
            {
                IdPropuesta = id,
                MotivoDenuncia = motivos,
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
            if(DenunciasService.Denuncie(denuncia.IdPropuesta))
                ModelState.AddModelError("","Ya existe una denuncia de esta persona para esta propuesta");
            if (!ModelState.IsValid)
            {
                var motivos = MotivoDenunciaService.GetAll().Select(m => new SelectListItem
                {
                    Text = m.Descripcion,
                    Value = m.IdMotivoDenuncia.ToString(),
                }).ToList();
                motivos = motivos.Prepend(new SelectListItem
                {
                    Value = "0",
                    Text = "Seleccione un motivo",
                    Disabled = true,
                    Selected = true
                }).ToList();
                DenunciaViewModel viewModel = new DenunciaViewModel
                {
                    IdPropuesta = denuncia.IdPropuesta,
                    MotivoDenuncia = motivos,
                    NombrePropuesta = PropuestaService.GetById(denuncia.IdPropuesta).Nombre,
                };
                return View(viewModel);
            }

            DenunciasService.Crear(denuncia);
            return RedirectToAction("Inicio", "Inicio");
        }

        public ActionResult Donar(int id)
        {
            var propuesta = PropuestaService.GetById(id);
            DonarViewModel dvm = new DonarViewModel
            {
                Propuesta = propuesta,
                DonacionesMonetarias = DonacionesMonetariasService.GetById(id),
                DonacionesHorasTrabajo = DonacionesHorasTrabajoService.GetById(id),
                DonacionesInsumos = DonacionesInsumosService.GetById(id)
            };
            return View(dvm);
        }
    }
}