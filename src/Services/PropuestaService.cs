using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public class PropuestaService : BaseService<PropuestaService>
    {
        public static void AgregarPropuesta(Propuesta p)
        {
            p.IdUsuarioCreador = Helpers.SessionHelper.Usuario.IdUsuario;
            p.Estado = 0;
            p.FechaCreacion = System.DateTime.Today;

            

            Db.Propuesta.Add(p);
            Db.SaveChanges();
        }

        public static int TotalPropuestasActivas()
        {
            return Db.Propuesta.Count(x => x.Estado == 0 && x.IdUsuarioCreador == Helpers.SessionHelper.Usuario.IdUsuario);
        }
    }
}
