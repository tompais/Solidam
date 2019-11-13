using System;
using System.Globalization;
using System.Linq;
using System.Text;
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

            if(!string.IsNullOrEmpty(usuarioAModificar.Nombre) || !string.IsNullOrEmpty(usuarioAModificar.Apellido) || !string.IsNullOrEmpty(usuarioAModificar.Foto) || !string.IsNullOrEmpty(usuarioAModificar.UserName)) throw new PerfilUsuarioCompletadoException();

            usuarioAModificar.Nombre = model.Nombre;
            usuarioAModificar.Apellido = model.Apellido;
            usuarioAModificar.FechaNacimiento = model.FechaNacimiento;
            usuarioAModificar.Foto = model.Foto;
            usuarioAModificar.UserName = model.UserName;

            Db.CustomSaveChanges();

            return usuarioAModificar;
        }

        public string GenerarUserName(string nombre, string apellido)
        {
            var userName = new string($"{nombre.Trim().ToUpper()}.{apellido.Trim().ToUpper()}".Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray()).Normalize(NormalizationForm.FormC);

            var similarUser = Db.Usuarios.Where(u => u.UserName.StartsWith(userName)).ToList().LastOrDefault();

            if (similarUser == null) return userName;
            userName += ".";
            if (int.TryParse(similarUser.UserName.Replace(userName, string.Empty), out var numeroUsuario))
            {
                userName += ++numeroUsuario;
            }
            else
            {
                userName += 2;
            }

            return userName;
        }

        public Usuarios GetById(ulong id)
        {
            if (id <= 0) throw new IdNoValidoException(typeof(Usuarios), id);
            return Db.Usuarios.FirstOrDefault(usuario => usuario.IdUsuario == (int) id);
        }
    }
}