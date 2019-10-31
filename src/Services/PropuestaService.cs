using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Helpers;
using Models;

namespace Services
{
    public class PropuestaService : BaseService<PropuestaService>
    {
        public static void AgregarPropuesta(Propuestas p)
        {
            p.IdUsuarioCreador = SessionHelper.Usuario.IdUsuario;
            p.Estado = 0;
            p.FechaCreacion = DateTime.Today;

            Db.Propuestas.Add(p);
            Db.SaveChanges();
        }

        public static void Actualizar(Propuestas p)
        {
            Propuestas propuesta = GetById(p.IdPropuesta);

            Db.SaveChanges();
        }

        public static Propuestas GetById(int id)
        {
            return Db.Propuestas.Include("PropuestasDonacionesHorasTrabajo")
                .Include("PropuestasDonacionesInsumos")
                .Include("PropuestasDonacionesMonetarias")
                .FirstOrDefault(p => p.IdPropuesta == id);
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
                .Where(p => p.Estado == (int)PropuestaEstado.Abierta).OrderByDescending(p => p.Valoracion).Take(5).ToList();
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
                Db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var errors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in errors.ValidationErrors)
                    {
                        // get the error message 
                        string errorMessage = validationError.ErrorMessage;
                    }
                }
            }
        }

        public static List<Propuestas> ObtenerPropuestasPorNombreYUsuario(string nombre)
        {
            var propuestas = Db.Propuestas.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
                propuestas = propuestas.Where(p => p.Nombre.Contains(nombre));

            if (SessionHelper.Usuario != null)
                propuestas = propuestas.Where(u => u.Usuarios.IdUsuario != SessionHelper.Usuario.IdUsuario);

            return propuestas.ToList();
        }

        public static List<Propuestas> ObtenerPropuestasUsuario(int id)
        {
            var misPropuestas = Db.Propuestas.AsQueryable();

            if (SessionHelper.Usuario != null)
                misPropuestas = misPropuestas.Where(u => u.Usuarios.IdUsuario == SessionHelper.Usuario.IdUsuario);

            return misPropuestas.ToList();
        }

        public static void Finalizar(int idPropuesta)
        {
            var donacion = GetById(idPropuesta);
            donacion.Estado = (int) PropuestaEstado.Cerrada;
            Db.SaveChanges();
        }
    }
}
