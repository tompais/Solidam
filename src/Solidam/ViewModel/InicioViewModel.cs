using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Solidam.ViewModel
{
    public class InicioViewModel
    {
        public List<Propuestas> PropuestasTop5 { get; set; }
        public Usuarios Usuario { get; set; }
        public List<Propuestas> PropuestasActivas { get; set; }
    }
}