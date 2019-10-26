using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace Solidam.ViewModel
{
    public class DenunciaViewModel
    {
        public int IdPropuesta { get; set; }
        public string NombrePropuesta { get; set; }
        public List<SelectListItem> MotivoDenuncia { get; set; }
        public Denuncias Denuncia { get; set; }

    }
}