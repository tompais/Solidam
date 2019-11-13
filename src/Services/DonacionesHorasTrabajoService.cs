using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Models;

namespace Services
{
    public class DonacionesHorasTrabajoService : BaseService<DonacionesHorasTrabajoService>
    {
        public static void Crear(DonacionesHorasTrabajo model)
        {
            model.IdUsuario = SessionHelper.Usuario.IdUsuario;
            Db.DonacionesHorasTrabajo.Add(model);
            Db.CustomSaveChanges();
        }

        public static List<DonacionesHorasTrabajo> GetById(int idPropuesta)
        {
            return Db.PropuestasDonacionesHorasTrabajo.Include("DonacionesHorasTrabajo").FirstOrDefault(dm => dm.IdPropuesta == idPropuesta)?.DonacionesHorasTrabajo.ToList();
        }
    }
}
