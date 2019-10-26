using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Enums;
using Helpers;
using Models;
using MotivoDenuncia = Models.MotivoDenuncia;

namespace Services
{
    public class DenunciasService : BaseService<DenunciasService>
    {
        public static void Crear(Denuncias model)
        {
            if (model.IdUsuario != 0)
            {
                Db.Denuncias.Add(model);
                Db.SaveChanges();
            }
        }

        public static bool Denuncie(int idPropuesta)
        {
            return Db.Denuncias.Any(d => d.IdPropuesta == idPropuesta && d.IdUsuario == SessionHelper.Usuario.IdUsuario);
        }
    }
}
