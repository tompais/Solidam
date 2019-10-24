using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [MetadataType(typeof(UsuariosMetadata))]
    public partial class Usuarios
    {
        public string EmailLogin { get; set; }

        public string PasswordLogin { get; set; }

        public string Repassword { get; set; }
    }
}
