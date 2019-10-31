using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

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
        public string Foto { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Ingrese una Foto de Perfil")]
        public HttpPostedFileBase ProfilePicFile { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }

        public UsuarioPerfil()
        {
        }

        public UsuarioPerfil(Usuarios usuario)
        {
            Nombre = usuario.Nombre;
            Apellido = usuario.Apellido;
            FechaNacimiento = usuario.FechaNacimiento;
            Foto = usuario.Foto;
            Email = usuario.Email;
            UserName = usuario.UserName;
        }
    }
}