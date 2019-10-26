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
    }
}
