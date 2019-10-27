using System.Linq;
using Exceptions;
using Interfaces;
using Models;

namespace Services
{
    public class UsuarioService : BaseService<UsuarioService>, IGetByIdService<Usuarios>, IPutService<Usuarios>
    {
        private UsuarioService()
        {
        }

        public Usuarios Put(Usuarios model)
        {
            var usuarioAModificar = GetById((ulong) model.IdUsuario);

            if(usuarioAModificar == null) throw new EntidadNoEncontradaException(typeof(Usuarios));

            usuarioAModificar.Nombre = model.Nombre;
            usuarioAModificar.Apellido = model.Apellido;
            usuarioAModificar.FechaNacimiento = model.FechaNacimiento;

            Db.SaveChanges();

            return usuarioAModificar;
        }

        public Usuarios GetById(ulong id)
        {
            if (id <= 0) throw new IdNoValidoException(typeof(Usuarios), id);
            return Db.Usuarios.FirstOrDefault(usuario => usuario.IdUsuario == (int) id);
        }
    }
}