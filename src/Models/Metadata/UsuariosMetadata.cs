using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UsuariosMetadata
    {
        [Required(ErrorMessage = "Ingrese su Email")]
        [EmailAddress(ErrorMessage = "Formato de Email Incorrecto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese su Contraseña")]
        public string Password { get; set; }
    }
}
