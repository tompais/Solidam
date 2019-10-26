using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public static ValidationResult ValidarMayoriaEdad(object value, ValidationContext context)
        {
            var usuario = context.ObjectInstance as UsuariosRegister;

            var edad = DateTime.Now.Year - usuario.FechaNacimiento.Year;

            return edad < 18 ? new ValidationResult("Debes tener mas de 18 años") : ValidationResult.Success;
        }

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
