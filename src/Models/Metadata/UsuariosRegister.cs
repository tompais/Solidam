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
    public class UsuariosRegister
    {

        [Required(ErrorMessage = "Ingrese su Email")]
        [EmailAddress(ErrorMessage = "Formato de Email Incorrecto")]
        [CustomValidation(typeof(UsuariosMetadata), "ValidarEmailUnico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese su Contraseña")]
        [CustomValidation(typeof(UsuariosMetadata), "ValidarPass")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirme su Contraseña")]
        [Compare(nameof(Password), ErrorMessage = "Sus contraseñas no coinciden")]
        public string Repassword { get; set; }

        [Required(ErrorMessage = "Ingrese su fecha de nacimiento")]
        [CustomValidation(typeof(UsuariosMetadata), "ValidarMayoriaEdad")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaNacimiento { get; set; }


    }
}
