using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Enums;
using Helpers;
using Models;

namespace Services
{
    public class PropuestaService : BaseService<PropuestaService>
    {
        public static void AgregarPropuesta(Propuestas p)
        {
            p.IdUsuarioCreador = 6;
            p.Estado = 0;
            p.FechaCreacion = System.DateTime.Today;

            Db.Propuestas.Add(p);
            Db.SaveChanges();
        }

        public static Propuestas GetById(int id)
        {
            return Db.Propuestas.Include("PropuestasDonacionesHorasTrabajo")
                .Include("PropuestasDonacionesInsumos")
                .Include("PropuestasDonacionesMonetarias")
                .FirstOrDefault(p => p.IdPropuesta == id);
        }

        //public static Propuestas GetPropuestaCompletaById(int id)
        //{

            
        //}
        public static int TotalPropuestasActivas()
        {
            return Db.Propuestas.Count(x => x.Estado == 0 && x.IdUsuarioCreador == Helpers.SessionHelper.Usuario.IdUsuario);
        }

        public static List<Propuestas> GetPropuestasMasValoradas()
        {
            return Db.Propuestas.Include("PropuestasDonacionesHorasTrabajo")
                .Include("PropuestasDonacionesInsumos")
                .Include("PropuestasDonacionesMonetarias")
                .Include("Usuarios")
                .Where(p => p.Estado == (int)PropuestaEstado.Abierta).OrderByDescending(p => p.Valoracion).Take(5).ToList();
        }

        public static decimal GetDineroDonadoByPropuestaId(int id)
        {
            var donaciones = Db.DonacionesMonetarias.Where(dm => dm.IdPropuestaDonacionMonetaria == id).ToList();
            if (!donaciones.Any()) return 0;
            return donaciones.Sum(dm => dm.Dinero);
        }

        public static void PutPorcentajeAceptacion(int id)
        {
            var valoraciones = Db.PropuestasValoraciones.Where(pv => pv.IdPropuesta == id).ToList();
            var cantValoraciones = valoraciones.Count;
            var cantMg = valoraciones.Count(v => v.Valoracion);
            var propuesta = GetById(id);
            propuesta.Valoracion = cantValoraciones == 0 ? 0 : cantMg * 100 / cantValoraciones;
            Db.SaveChanges();
        }

        public static List<DonacionesMonetarias> GetDonacionesMonetariasById(int idPropuesta)
        {
            return Db.PropuestasDonacionesMonetarias.Include("DonacionesMonetarias").FirstOrDefault(pdm => pdm.IdPropuesta == idPropuesta)
                ?.DonacionesMonetarias.ToList();
        }
        public static List<DonacionesHorasTrabajo> GetDonacionesHorasTrabajoById(int idPropuesta)
        {
            return Db.PropuestasDonacionesHorasTrabajo.Include("DonacionesHorasTrabajo").FirstOrDefault(dm => dm.IdPropuesta == idPropuesta)?.DonacionesHorasTrabajo.ToList();
        }
        public static List<DonacionesInsumos> GetDonacionesInsumosById(int idPropuesta)
        {
            var itemsNecesitados = Db.PropuestasDonacionesInsumos.Include("DonacionesInsumos").Where(dm => dm.IdPropuesta == idPropuesta).ToList();
            List<DonacionesInsumos> donaciones = new List<DonacionesInsumos>();
            foreach (PropuestasDonacionesInsumos item in itemsNecesitados)
            {
                donaciones.AddRange(item.DonacionesInsumos);
            }

            return donaciones;
        }
    }
}
