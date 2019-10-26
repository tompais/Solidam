using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Solidam.ViewModel
{
    public class DenunciaViewModel
    {
        public int IdPropuesta { get; set; }
        public string NombrePropuesta { get; set; }
        public List<MotivoDenuncia> MotivoDenuncia { get; set; }
    }
}