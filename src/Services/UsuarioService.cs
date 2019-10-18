using System;
using System.Linq;
using Helpers;
using Interfaces;
using Models;

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
            usuario.Token = "19E41C31E74A4526";

            SolidamEntities.Instance.Usuario.Add(usuario);

            SolidamEntities.Instance.SaveChanges();
        }
    }
}