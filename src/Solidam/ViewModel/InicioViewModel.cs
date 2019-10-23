using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Solidam.ViewModel
{
    public class InicioViewModel
    {
        public List<Propuestas> Propuestas { get; set; }
        public Usuarios Usuario { get; set; }
    }
}