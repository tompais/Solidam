using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using Utils;

namespace Models
{
    public class UsuariosMetadata
    {
        
        public static ValidationResult ValidarEmailUnico(object value, ValidationContext context)
        {
            var usuario = context.ObjectInstance as UsuariosRegister;

            var existeEmail = SolidamEntities.Instance.Usuarios.Any(o => o.Email == usuario.Email);

            if (existeEmail)
            {
                return new ValidationResult(string.Format("El Email {0} ya está siendo usado.", usuario.Email));
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidarMayoriaEdad(object value, ValidationContext context)
        {
            var usuario = context.ObjectInstance as UsuariosRegister;

            var edad = DateTime.Now.Year - usuario.FechaNacimiento.Year;

            if (edad < 18)
            {
                return new ValidationResult(string.Format("Debes tener mas de 18 años"));
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidarLogueo(object value, ValidationContext context)
        {
            var usuario = context.ObjectInstance as UsuariosLogin;

            usuario.Password = Sha1.GetSHA1(usuario.Password);

            var usuarioCorrecto = SolidamEntities.Instance.Usuarios.FirstOrDefault(u =>
                u.Email == usuario.Email && u.Password == usuario.Password);

            if (usuarioCorrecto == null)
            {
                return new ValidationResult(string.Format("Email/Contraña Incorrecto"));
            }

            if (usuarioCorrecto.Activo == false)
            {
                return new ValidationResult(string.Format("Su usuario está inactivo. Actívelo desde el email recibido"));
            }

            return ValidationResult.Success;
        }


    }
}
