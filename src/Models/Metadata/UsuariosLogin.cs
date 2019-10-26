using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models
{
    [MetadataType(typeof(UsuariosMetadata))]
    public class UsuariosLogin
    {
        [NotMapped]
        [Required(ErrorMessage = "Ingrese su Email")]
        [EmailAddress(ErrorMessage = "Formato de Email Incorrecto")]
        public string Email { get; set; }

        [NotMapped]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Ingrese su Contraseña")]
        [CustomValidation(typeof(UsuariosMetadata), "ValidarLogueo")]
        public string Password { get; set; }

    }
}
