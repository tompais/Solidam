﻿using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enums;
using Helpers;
using Models;
using Services;
using Solidam.ViewModel;
using MotivoDenuncia = Models.MotivoDenuncia;
using System.Collections.Generic;

namespace Solidam.Controllers
{
    public class PropuestaController : BaseController
    {
        public ActionResult CrearPropuesta()
        {
            if (PropuestaService.TotalPropuestasActivas() == 3)
                return RedirectToAction("Inicio", "Inicio");

            return View();
        }

        public ActionResult ModificarPropuesta(int id)
        {
            return View(PropuestaService.GetById(id));
        }

        [HttpPost]
        public ActionResult Modificar(Propuestas p, HttpPostedFileBase foto, string fotoVieja)
        {
            ModelState.Remove("Foto");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View("ModificarPropuesta");
            }

            if (foto != null && foto.ContentLength > 0)
            {
                string path = Server.MapPath("~/Images/Views/Propuesta/");

                p.Foto = Path.GetFileName(foto.FileName);

                PropuestaService.Actualizar(p);

                foto.SaveAs(path + Path.GetFileName(foto.FileName));
            }
            else
                PropuestaService.Actualizar(p);

            return View();
        }

        public ActionResult Buscar(string nombre)
        {
            ViewBag.palabra = nombre;

            var propuestaBuscadas = PropuestaService.ObtenerPropuestasPorNombreYUsuario(nombre);

            return View("PropuestasBuscadas", propuestaBuscadas);
        }

        public ActionResult MisPropuestas()
        {

            var misPropuestas = PropuestaService.ObtenerPropuestasUsuario(SessionHelper.Usuario.IdUsuario, null);

            var cont = 0;

            foreach (var propuesta in misPropuestas)
            {
                if (propuesta.Estado == 0)
                {
                    cont++;
                }
            }

            ViewBag.propuestasCreadas = cont;

            return View("MisPropuestas", misPropuestas);
        }

        public ActionResult MisPropuestasActivas()
        {
            var activa = "activa";

            ViewBag.Activa = activa;

            var misPropuestasActivas = PropuestaService.ObtenerPropuestasUsuario(SessionHelper.Usuario.IdUsuario, activa);

            var cont = 0;

            foreach (var propuesta in misPropuestasActivas)
            {
                if (propuesta.Estado == 0)
                {
                    cont++;
                }
            }

            ViewBag.propuestasCreadas = cont;

            return View("MisPropuestas", misPropuestasActivas);
        }


        [HttpPost]
        public ActionResult Crear(Propuestas p, HttpPostedFileBase foto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View("CrearPropuesta");
            }

            string path = Server.MapPath("~/Images/Views/Propuesta/");

            p.Foto = Path.GetFileName(foto.FileName);

            PropuestaService.AgregarPropuesta(p);

            foto.SaveAs(path + Path.GetFileName(foto.FileName));

            return RedirectToAction("MisPropuestas", "Propuesta");
        }

        [HttpPost]
        public ActionResult Valorar(string mg, string nmg, PropuestasValoraciones pv)
        {
            var propuestasValoraciones = new PropuestasValoraciones
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
                TempData["pendingRoute"] = Url.Action("Detalle", "Propuesta", new { id });
                return RedirectToAction("Iniciar", "Seguridad");
            }
            var propuesta = PropuestaService.GetById(id);
            var propuestaDetalleViewModel = new PropuestaDetalleViewModel
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
                Value = m.IdMotivoDenuncia.ToString()
            }).ToList();
            motivos = motivos.Prepend(new SelectListItem
            {
                Value = "0",
                Text = "Seleccione un motivo",
                Disabled = true,
                Selected = true
            }).ToList();
            var viewModel = new DenunciaViewModel
            {
                IdPropuesta = id,
                MotivoDenuncia = motivos,
                NombrePropuesta = PropuestaService.GetById(id).Nombre
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Denunciar(Denuncias denuncia)
        {
            denuncia.FechaCreacion = DateTime.Now;
            denuncia.IdUsuario = SessionHelper.Usuario.IdUsuario;
            denuncia.Estado = (int)DenunciaEstado.EnRevision;
            if (DenunciasService.Denuncie(denuncia.IdPropuesta))
                ModelState.AddModelError("", "Ya existe una denuncia de esta persona para esta propuesta");
            if (!ModelState.IsValid)
            {
                var motivos = MotivoDenunciaService.GetAll().Select(m => new SelectListItem
                {
                    Text = m.Descripcion,
                    Value = m.IdMotivoDenuncia.ToString()
                }).ToList();
                motivos = motivos.Prepend(new SelectListItem
                {
                    Value = "0",
                    Text = "Seleccione un motivo",
                    Disabled = true,
                    Selected = true
                }).ToList();
                var viewModel = new DenunciaViewModel
                {
                    IdPropuesta = denuncia.IdPropuesta,
                    MotivoDenuncia = motivos,
                    NombrePropuesta = PropuestaService.GetById(denuncia.IdPropuesta).Nombre
                };
                return View(viewModel);
            }

            DenunciasService.Crear(denuncia);
            return RedirectToAction("Inicio", "Inicio");
        }

        public ActionResult Donar(int id)
        {
            var propuesta = PropuestaService.GetById(id);
            var dvm = new DonarViewModel
            {
                Propuesta = propuesta,
                DonacionesMonetarias = DonacionesMonetariasService.GetById(id),
                DonacionesHorasTrabajo = DonacionesHorasTrabajoService.GetById(id),
                DonacionesInsumos = DonacionesInsumosService.GetById(id)
            };
            return View(dvm);
        }

        public ActionResult Completada()
        {
            return View(PropuestaService.GetById(SessionHelper.IdPropuestaCompletada));
        }
    }
}