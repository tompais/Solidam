using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Solidam.ViewModel
{
    public class DonarViewModel
    {
        public Propuestas Propuesta { get; set; }
        public List<DonacionesMonetarias> DonacionesMonetarias { get; set; }
        public List<DonacionesHorasTrabajo> DonacionesHorasTrabajo { get; set; }
        public List<DonacionesInsumos> DonacionesInsumos { get; set; }
        public DonacionesMonetarias DonacionMonetaria { get; set; }
    }
}