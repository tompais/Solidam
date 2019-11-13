using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Enums;
using Helpers;
using Interfaces;
using Models;

namespace Services
{
    public class PropuestaService : BaseService<PropuestaService>, IPutService<Propuestas>
    {
        public static void AgregarPropuesta(Propuestas p)
        {
            p.IdUsuarioCreador = SessionHelper.Usuario.IdUsuario;
            p.Estado = (int)PropuestaEstado.Abierta;
            p.FechaCreacion = DateTime.Today;
            p.Valoracion = 0;
            Db.Propuestas.Add(p);
            Db.CustomSaveChanges();
        }

        public static void Actualizar(Propuestas p)
        {
            var propuesta = GetById(p.IdPropuesta);

            propuesta.Nombre = p.Nombre;
            propuesta.Descripcion = p.Descripcion;
            propuesta.FechaFin = p.FechaFin;
            propuesta.PropuestasReferencias.ElementAt(0).Nombre = p.PropuestasReferencias.ElementAt(0).Nombre;
            propuesta.PropuestasReferencias.ElementAt(0).Telefono = p.PropuestasReferencias.ElementAt(0).Telefono;
            propuesta.PropuestasReferencias.ElementAt(1).Nombre = p.PropuestasReferencias.ElementAt(1).Nombre;
            propuesta.PropuestasReferencias.ElementAt(1).Telefono = p.PropuestasReferencias.ElementAt(1).Telefono;
            propuesta.TelefonoContacto = p.TelefonoContacto;

            if (p.Foto != null)
                propuesta.Foto = p.Foto;

            switch (propuesta.TipoDonacion)
            {
                case (int)TipoDonacion.Monetaria:
                    propuesta.PropuestasDonacionesMonetarias.ElementAt(0).Dinero = p.PropuestasDonacionesMonetarias.ElementAt(0).Dinero;
                    propuesta.PropuestasDonacionesMonetarias.ElementAt(0).CBU = p.PropuestasDonacionesMonetarias.ElementAt(0).CBU;
                    break;
                case (int)TipoDonacion.Insumos:
                    {
                        for (var i = 0; i < p.PropuestasDonacionesInsumos.Count; i++)
                        {
                            propuesta.PropuestasDonacionesInsumos.ElementAt(i).Nombre = p.PropuestasDonacionesInsumos.ElementAt(i).Nombre;
                            propuesta.PropuestasDonacionesInsumos.ElementAt(i).Cantidad = p.PropuestasDonacionesInsumos.ElementAt(i).Cantidad;
                        }

                        break;
                    }
                default:
                    propuesta.PropuestasDonacionesHorasTrabajo.ElementAt(0).CantidadHoras = p.PropuestasDonacionesHorasTrabajo.ElementAt(0).CantidadHoras;
                    propuesta.PropuestasDonacionesHorasTrabajo.ElementAt(0).Profesion = p.PropuestasDonacionesHorasTrabajo.ElementAt(0).Profesion;
                    break;
            }

            Db.CustomSaveChanges();
        }

        public static Propuestas GetById(int id)
        {
            return Db.Propuestas.Include("PropuestasDonacionesHorasTrabajo")
                .Include("PropuestasDonacionesInsumos")
                .Include("PropuestasDonacionesMonetarias")
                .FirstOrDefault(p => p.IdPropuesta == id);
        }

        public static List<Propuestas> TodasLasPropuestasActivas()
        {
            return Db.Propuestas.Where(p => p.Estado == 0).ToList();
        }

        public static int TotalPropuestasActivas()
        {
            return Db.Propuestas.Count(x => x.Estado == 0 && x.IdUsuarioCreador == SessionHelper.Usuario.IdUsuario);
        }

        public static List<Propuestas> GetPropuestasMasValoradas()
        {
            return Db.Propuestas.Include("PropuestasDonacionesHorasTrabajo")
                .Include("PropuestasDonacionesInsumos")
                .Include("PropuestasDonacionesMonetarias")
                .Include("Usuarios")
                .Where(p => p.Estado == (int)PropuestaEstado.Abierta && p.FechaFin > DateTime.Today).OrderByDescending(p => p.Valoracion).Take(5).ToList();
        }

        public static void PutPorcentajeAceptacion(int id)
        {
            var valoraciones = Db.PropuestasValoraciones.Where(pv => pv.IdPropuesta == id).ToList();
            var cantValoraciones = valoraciones.Count;
            var cantMg = valoraciones.Count(v => v.Valoracion);
            var propuesta = GetById(id);
            propuesta.Valoracion = cantValoraciones == 0 ? 0 : cantMg * 100 / cantValoraciones;
            try
            {
                Db.CustomSaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var errors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in errors.ValidationErrors)
                    {
                        // get the error message 
                        var errorMessage = validationError.ErrorMessage;
                    }
                }
            }
        }

        public static List<Propuestas> ObtenerPropuestasPorNombreYUsuario(string palabra)
        {
            var propuestasQuery = Db.Propuestas.Where(p => p.Estado == (int)PropuestaEstado.Abierta && p.FechaFin > DateTime.Today).AsQueryable();

            if (!String.IsNullOrEmpty(palabra))
            {

                propuestasQuery = propuestasQuery
                    .Where(p => p.Nombre.ToLower().Contains(palabra) || p.Usuarios.Nombre.ToLower().Contains(palabra.ToLower()) || p.Usuarios.Apellido.ToLower().Contains(palabra.ToLower()))
                    .OrderBy(p => p.FechaFin).ThenBy(p => p.Valoracion);

                if (SessionHelper.Usuario != null)
                    propuestasQuery =
                        propuestasQuery.Where(u => u.Usuarios.IdUsuario != SessionHelper.Usuario.IdUsuario);
            }

            return propuestasQuery.ToList();

        }

        public static List<Propuestas> ObtenerPropuestasUsuario(int id, string activa)
        {
            var misPropuestas = Db.Propuestas.AsQueryable();

            if (SessionHelper.Usuario != null)
                misPropuestas = misPropuestas.Where(u => u.Usuarios.IdUsuario == SessionHelper.Usuario.IdUsuario);

            if (!string.IsNullOrEmpty(activa))
            {
                misPropuestas = misPropuestas.Where(p => p.Estado == (int)PropuestaEstado.Abierta && p.FechaFin > DateTime.Today);
            }

            return misPropuestas.ToList();
        }



        public static void Finalizar(int idPropuesta)
        {
            var donacion = GetById(idPropuesta);
            donacion.Estado = (int)PropuestaEstado.Cerrada;
            Db.CustomSaveChanges();
        }

        public Propuestas Put(Propuestas model)
        {
            throw new NotImplementedException();
        }

        public void PonerPropuestaEnRevision(int idPropuesta)
        {
            var propuesta = GetById(idPropuesta);
            if (propuesta.Denuncias.Count(denuncia => denuncia.Estado == (int)DenunciaEstado.EnRevision) < 5 ||
                propuesta.Denuncias.Any(denuncia => denuncia.Estado == (int)DenunciaEstado.Aceptada)) return;
            propuesta.Estado = (int)PropuestaEstado.EnRevision;
            Db.CustomSaveChanges();

        }
    }
}
