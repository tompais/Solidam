using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required(ErrorMessage = "Ingrese su Contraseña")]
        [CustomValidation(typeof(UsuariosMetadata), "ValidarLogueo")]
        public string Password { get; set; }

    }
}
