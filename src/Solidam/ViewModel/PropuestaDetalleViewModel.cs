using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Solidam.ViewModel
{
    public class PropuestaDetalleViewModel
    {
        public Propuestas Propuesta { get; set; }
        public bool Denuncie { get; set; }
        public PropuestasValoraciones Valoracion { get; set; }
    }
}