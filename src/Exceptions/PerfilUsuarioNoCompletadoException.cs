using Enums;

namespace Exceptions
{
    public class PerfilUsuarioNoCompletadoException : SolidamException
    {
        public override ErrorCode ErrorCode => ErrorCode.PerfilUsuarioNoCompletado;

        public override string Message => "Su perfil de usuario no está completo";
    }
}