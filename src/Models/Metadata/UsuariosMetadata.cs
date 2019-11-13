using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Utils;

namespace Models
{
    public class UsuariosMetadata : BaseMetadata
    {
        
        public static ValidationResult ValidarEmailUnico(object value, ValidationContext context)
        {
            var usuario = context.ObjectInstance as UsuariosRegister;

            var existeEmail = Db.Usuarios.Any(o => o.Email == usuario.Email);

            return existeEmail ? new ValidationResult($"El Email {usuario.Email} ya está siendo usado.") : ValidationResult.Success;
        }

        public static ValidationResult ValidarPass(object value, ValidationContext context)
        {
            var usuario = context.ObjectInstance as UsuariosRegister;

            var password = usuario.Password;

            if (!(password.Any(char.IsUpper) && password.Any(char.IsDigit) && usuario.Password.Length >= 8))
            {
                return new ValidationResult("Su contraseña debe tener 8 caracteres, una mayuscula y un numero");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidarMayoriaEdad(object value, ValidationContext context) => context.ObjectInstance is UsuariosRegister usuario && (default(DateTime) + (DateTime.Now - usuario.FechaNacimiento)).Year - 1 < 18 ? new ValidationResult("Debes tener más de 18 años") : ValidationResult.Success;

        public static ValidationResult ValidarLogueo(object value, ValidationContext context)
        {
            var usuario = context.ObjectInstance as UsuariosLogin;

            usuario.Password = Sha1.GetSHA1(usuario.Password);

            var usuarioCorrecto = Db.Usuarios.FirstOrDefault(u =>
                u.Email == usuario.Email && u.Password == usuario.Password);

            if (usuarioCorrecto == null)
            {
                return new ValidationResult("Email/Contraña Incorrecto");
            }

            return usuarioCorrecto.Activo == false ? new ValidationResult("Su usuario está inactivo. Actívelo desde el email recibido") : ValidationResult.Success;
        }
    }
}
