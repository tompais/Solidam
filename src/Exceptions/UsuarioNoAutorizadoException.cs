using Enums;

namespace Exceptions
{
    public class UsuarioNoAutorizadoException : SolidamException
    {
        public override ErrorCode ErrorCode => ErrorCode.UsuarioNoAutorizado;

        public override string Message =>
            "No se encuentra autorizado para acceder a esta sección con los permisos de su usuario";
    }
}