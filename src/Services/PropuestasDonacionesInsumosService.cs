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
            Db.SaveChanges();
        }

        public static int GetPropuestaId(int idPropuestaDonacionInsumo)
        {
            return Db.PropuestasDonacionesInsumos
                .FirstOrDefault(p => p.IdPropuestaDonacionInsumo == idPropuestaDonacionInsumo).IdPropuesta;
        }
    }
}
