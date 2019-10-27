using Enums;

namespace Exceptions
{
    public class PerfilInvalidoException : SolidamException
    {
        public override ErrorCode ErrorCode => ErrorCode.PerfilInvalido;

        public override string Message => "El perfil ingresado no es válido";
    }
}