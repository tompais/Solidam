using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [MetadataType(typeof(UsuariosMetadata))]
    public class UsuarioPerfil
    {
        [Required(ErrorMessage = "Ingrese su nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese su apellido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Ingrese su fecha de nacimiento")]
        [CustomValidation(typeof(UsuariosMetadata), "ValidarMayoriaEdad")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Ingrese una Foto de Perfil")]
        public string Foto { get; set; }

        public UsuarioPerfil(string nombre, string apellido, DateTime fechaNacimiento, string foto)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            Foto = foto;
        }

        public UsuarioPerfil(Usuarios usuario)
        {
            Nombre = usuario.Nombre;
            Apellido = usuario.Apellido;
            FechaNacimiento = usuario.FechaNacimiento;
            Foto = usuario.Foto;
        }
    }
}