using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DonacionesInsumoDto
    {
        public int IdPropuestaDonacionInsumo { get; set; }
        public int IdPropuesta { get; set; }
        public string Seleccionado { get; set; }
        public string Nombre { get; set; }
    }
}
