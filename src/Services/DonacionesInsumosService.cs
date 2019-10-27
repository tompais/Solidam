using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Models;

namespace Services
{
    public class DonacionesInsumosService : BaseService<DonacionesInsumosService>
    {
        public static void Crear(DonacionesInsumos model)
        {
            Validar(model);
            model.IdUsuario = SessionHelper.Usuario.IdUsuario;
            Db.DonacionesInsumos.Add(model);
            Db.SaveChanges();
        }
        public static List<DonacionesInsumos> GetById(int idPropuesta)
        {
            var itemsNecesitados = Db.PropuestasDonacionesInsumos.Include("DonacionesInsumos").Where(dm => dm.IdPropuesta == idPropuesta).ToList();
            List<DonacionesInsumos> donaciones = new List<DonacionesInsumos>();
            foreach (PropuestasDonacionesInsumos item in itemsNecesitados)
            {
                donaciones.AddRange(item.DonacionesInsumos);
            }

            return donaciones;
        }

        private static void Validar(DonacionesInsumos model)
        {
            var objetivo = Db.PropuestasDonacionesInsumos
                .FirstOrDefault(pdi => pdi.IdPropuestaDonacionInsumo == model.IdPropuestaDonacionInsumo)?.Cantidad;
            var obtenido = Db.DonacionesInsumos
                .Where(di => di.IdPropuestaDonacionInsumo == model.IdPropuestaDonacionInsumo).Sum(d => d.Cantidad);
            var restante = objetivo - obtenido;
            if(model.Cantidad > restante || model.Cantidad <= 0)
                throw new Exception();
        }
    }
}
