using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Enums;
using Helpers;
using Models;

namespace Services
{
    public class PropuestasDonacionesInsumosService : BaseService<PropuestasDonacionesInsumosService>
    {
        public static void Crear(PropuestasDonacionesInsumos model)
        {
            
        }

        public static PropuestasDonacionesInsumos GetById(int id)
        {
            return Db.PropuestasDonacionesInsumos.FirstOrDefault(p => p.IdPropuestaDonacionInsumo == id);
        }
        public static void Delete(int id)
        {
            Db.PropuestasDonacionesInsumos.Remove(GetById(id));
        }

        public static void Update(DonacionesInsumos donacion)
        {
            var propuesta = Db.PropuestasDonacionesInsumos.FirstOrDefault(pdi =>
                pdi.IdPropuestaDonacionInsumo == donacion.IdPropuestaDonacionInsumo);
            if(propuesta == null) throw new Exception();
            propuesta.Cantidad -= donacion.Cantidad;
            Db.CustomSaveChanges();
        }

        public static int GetPropuestaId(int idPropuestaDonacionInsumo)
        {
            return Db.PropuestasDonacionesInsumos
                .FirstOrDefault(p => p.IdPropuestaDonacionInsumo == idPropuestaDonacionInsumo).IdPropuesta;
        }

        public static bool EsCompletada(List<DonacionesInsumos> donaciones)
        {
            var idPropuesta = donaciones[0].PropuestasDonacionesInsumos.IdPropuesta;
            var objetos = Db.PropuestasDonacionesInsumos.Where(p => p.IdPropuesta == idPropuesta).ToList();
            foreach (PropuestasDonacionesInsumos donacion in objetos)
            {
                var objetivo = Db.PropuestasDonacionesInsumos
                    .FirstOrDefault(p => p.IdPropuestaDonacionInsumo == donacion.IdPropuestaDonacionInsumo)
                    ?.Cantidad;
                var donacionesHechas = 
                    Db.DonacionesInsumos.Where(d => d.IdPropuestaDonacionInsumo == donacion.IdPropuestaDonacionInsumo).ToList();
                var obtenido = donacionesHechas.Count == 0 ? 0 : donacionesHechas.Sum(d => d.Cantidad);
                if (objetivo != obtenido)
                    return false;
            }
            return true;
        }
    }
}
