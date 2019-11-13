using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Enums;
using Helpers;
using Models;

namespace Services
{
    public class PropuestasDonacionesMonetariasService : BaseService<PropuestasDonacionesMonetariasService>
    {
        public static void Crear(PropuestasDonacionesMonetarias model)
        {
            
        }

        public static PropuestasDonacionesMonetarias GetById(int id)
        {
            return Db.PropuestasDonacionesMonetarias.FirstOrDefault(p => p.IdPropuestaDonacionMonetaria == id);
        }
        public static void Delete(int id)
        {
            Db.PropuestasDonacionesMonetarias.Remove(GetById(id));
        }

        //public static void Update(DonacionesMonetarias donacion)
        //{
        //    var propuesta = Db.PropuestasDonacionesMonetarias.FirstOrDefault(pdi =>
        //        pdi.IdPropuestaDonacionMonetaria == donacion.IdPropuestaDonacionMonetaria);
        //    if(propuesta == null) throw new Exception();
        //    propuesta. -= donacion.Cantidad;
        //    Db.SaveChanges();
        //}
        public static decimal GetDineroDonadoByPropuestaId(int id)
        {
            var donaciones = Db.DonacionesMonetarias.Where(dm => dm.IdPropuestaDonacionMonetaria == id).ToList();
            if (!donaciones.Any()) return 0;
            return donaciones.Sum(dm => dm.Dinero);
        }
        public static bool EsCompletada(DonacionesMonetarias donacionMonetaria)
        {
            var propuesta = Db.PropuestasDonacionesMonetarias.FirstOrDefault(p =>
                p.IdPropuestaDonacionMonetaria == donacionMonetaria.IdPropuestaDonacionMonetaria);
            var dineroObjetivo = propuesta?.Dinero;
            var dineroObtenido = Db.DonacionesMonetarias
                .Where(d => d.IdPropuestaDonacionMonetaria == donacionMonetaria.IdPropuestaDonacionMonetaria)
                .Sum(d => d.Dinero);
            if (dineroObtenido != dineroObjetivo) return false;
            PropuestaService.Finalizar(propuesta.IdPropuesta);
            return true;
        }
    }
}
