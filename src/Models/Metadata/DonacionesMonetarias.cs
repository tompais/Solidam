using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Models
{
    [MetadataType(typeof(DonacionesMonetariasMetadata))]

    public partial class DonacionesMonetarias
    {
        public HttpPostedFileBase File { get; set; }
    }
}
