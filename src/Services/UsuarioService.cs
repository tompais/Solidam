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
    }
}