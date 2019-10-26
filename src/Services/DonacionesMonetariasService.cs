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
            FileHelper.GuardarArchivo(model.ArchivoTransferencia);
            model.IdUsuario = SessionHelper.Usuario.IdUsuario;
            Db.DonacionesMonetarias.Add(model);
            Db.SaveChanges();
        }
    }
}
