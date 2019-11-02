using Enums;

namespace Exceptions
{
    public class UsuarioLogueadoException : SolidamException
    {
        public override ErrorCode ErrorCode => ErrorCode.UsuarioLogueado;
        public override string Message => "El usuario ya se encuentra logueado";
    }
}