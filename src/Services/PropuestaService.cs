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
        public static void AgregarPropuesta(Propuestas p)
        {
            p.IdUsuarioCreador = Helpers.SessionHelper.Usuario.IdUsuario;
            p.Estado = 0;
            p.FechaCreacion = System.DateTime.Today;

            

            Db.Propuestas.Add(p);
            Db.SaveChanges();
        }

        public static int TotalPropuestasActivas()
        {
            return Db.Propuestas.Count(x => x.Estado == 0 && x.IdUsuarioCreador == Helpers.SessionHelper.Usuario.IdUsuario);
        }
    }
}
