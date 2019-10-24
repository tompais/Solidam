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
    public class UsuariosMetadata
    {
        [Required(ErrorMessage = "Ingrese su Email")]
        [EmailAddress(ErrorMessage = "Formato de Email Incorrecto")]
        [CustomValidation(typeof(UsuariosMetadata), "ValidarEmailUnico")]
        public string Email { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Ingrese su Email")]
        [EmailAddress(ErrorMessage = "Formato de Email Incorrecto")]
        public string EmailLogin { get; set; }

        [Required(ErrorMessage = "Ingrese su Contraseña")]
        //[RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,16}$", ErrorMessage = "Su contraseña debe tener letras, (minúsculas y mayúsculas) y números")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirme su Contraseña")]
        [Compare(nameof(Password), ErrorMessage = "Sus contraseñas no coinciden")]
        public string Repassword { get; set; }

        [Required(ErrorMessage = "Ingrese su fecha de nacimiento")]
        //[CustomValidation(typeof(UsuariosMetadata), "ValidarMayoriaEdad")]
        public DateTime FechaNacimiento { get; set; }

        public static ValidationResult ValidarEmailUnico (object value, ValidationContext context)
        {
            var usuario = context.ObjectInstance as Usuarios;

            var existeEmail = SolidamEntities.Instance.Usuarios.Any(o => o.Email == usuario.Email);

            if (existeEmail)
            {
                return new ValidationResult(string.Format("El Email {0} ya está siendo usado.", usuario.Email));
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidarMayoriaEdad (object value, ValidationContext context)
        {
            var usuario = context.ObjectInstance as Usuarios;

            if ((usuario.FechaNacimiento.Year - DateTime.Now.Year) > 18)
            {
                return new ValidationResult(string.Format("Debe ser mayor a 18 "));
            }

            return ValidationResult.Success;
        }
    }
}
