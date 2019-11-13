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
    public class MotivoDenunciaService : BaseService<MotivoDenunciaService>
    {
        public static void Crear(MotivoDenuncia model)
        {

        }

        public static MotivoDenuncia GetById(int id)
        {
            return Db.MotivoDenuncia.FirstOrDefault(p => p.IdMotivoDenuncia == id);
        }

        public static List<MotivoDenuncia> GetAll()
        {
            return Db.MotivoDenuncia.ToList();
        }
    }
}
