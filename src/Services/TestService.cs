using System;
using Interfaces;
using Models;

namespace Services
{
    public class TestService : BaseService<TestService>, IPostService<Usuario>
    {
        private TestService()
        {
        }

        public Usuario Post(Usuario model)
        {
            var usuario = new Usuario
            {
                Nombre = "MARCOS",
                Apellido = "PEREZ",
                FechaNacimiento = new DateTime(1978, 2, 1),
                UserName = "admin",
                Email = "admin@test.com",
                Password = "12121212",
                Foto = string.Empty,
                TipoUsuario = 1,
                FechaCracion = DateTime.Now,
                Activo = true,
                Token = "19E41C31E74A4526"
            };

            Db.Usuario.Add(usuario);

            return usuario;
        }
    }
}