using System;
using System.Collections.Generic;
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
    }
}
