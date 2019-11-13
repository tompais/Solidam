using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [MetadataType(typeof(DonacionesInsumosMetadata))]
    public partial class DonacionesInsumos
    {
        public bool Seleccionado { get; set; }
    }
}
