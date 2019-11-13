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
            Db.CustomSaveChanges();
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
            var donacionesObtenidas = Db.DonacionesInsumos
                .Where(d => d.IdPropuestaDonacionInsumo == model.IdPropuestaDonacionInsumo).ToList();
            var obtenido = 0;
            if (donacionesObtenidas.Count != 0) obtenido = donacionesObtenidas.Sum(d => d.Cantidad);
            var restante = objetivo - obtenido;
            if(model.Cantidad > restante || model.Cantidad <= 0)
                throw new Exception();
        }

        public static List<PropuestasDonacionesInsumos> GetByPropuestaId(int idPropuesta)
        {
            return Db.PropuestasDonacionesInsumos.Where(p => p.IdPropuesta == idPropuesta).ToList();
        }

        public static int GetCantidadDonadaByObjeto(int idPropuestaDonacionInsumo)
        {
            var donaciones = Db.DonacionesInsumos.Where(d => d.IdPropuestaDonacionInsumo == idPropuestaDonacionInsumo)
                .ToList();
            return donaciones.Count == 0 ? 0 : donaciones.Sum(d => d.Cantidad);
        }
    }
}
