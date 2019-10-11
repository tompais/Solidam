using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public class TestService
    {
        static Entities db = new Entities();

        public static List<MotivoDenuncia> Listar()
        {
            return db.MotivoDenuncia.ToList();
        }
    }
}
