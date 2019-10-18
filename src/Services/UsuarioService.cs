using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Helpers;
using Interfaces;
using Models;
using Utils;


namespace Services
{
    public class UsuarioService : BaseService<TestService>, IPostService<Usuario>
    {
        public UsuarioService()
        {

        }

        public Usuario Post(Usuario model)
        {
            return null;

        }

        public Usuario BuscarUsuario(Usuario usuario)
        {
            usuario.Password = Sha1.GetSHA1(usuario.Password);

            Usuario usuarioBuscado = SolidamEntities.Instance.Usuario.FirstOrDefault(u => u.Email == usuario.Email && u.Password == usuario.Password);

            if (usuarioBuscado != null)
            {
                SessionHelper.Usuario = usuarioBuscado;
            }

            return usuarioBuscado;
        }

        public void RegistrarUsuario(Usuario usuario)
        {

            usuario.Activo = false;
            usuario.FechaCracion = DateTime.Now;
            usuario.TipoUsuario = 2;
            usuario.Token = Guid.NewGuid().ToString();
            usuario.Password = Sha1.GetSHA1(usuario.Password);

            SolidamEntities.Instance.Usuario.Add(usuario);

            SolidamEntities.Instance.SaveChanges();

        }
    }
}