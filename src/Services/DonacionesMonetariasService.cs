using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Models;

namespace Services
{
    public class DonacionesMonetariasService : BaseService<DonacionesMonetariasService>
    {
        public static void Crear(DonacionesMonetarias model)
        {
            model.IdUsuario = SessionHelper.Usuario.IdUsuario;
            model.FechaCreacion = DateTime.Now;
            Db.DonacionesMonetarias.Add(model);
            Db.CustomSaveChanges();
        }

        public static List<DonacionesMonetarias> GetById(int idPropuesta)
        {
            return Db.PropuestasDonacionesMonetarias.Include("DonacionesMonetarias").FirstOrDefault(pdm => pdm.IdPropuesta == idPropuesta)
                ?.DonacionesMonetarias.ToList();
        }
        }
}
