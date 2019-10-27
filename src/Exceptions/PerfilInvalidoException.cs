using Enums;

namespace Exceptions
{
    public class PerfilInvalidoException : SolidamException
    {
        protected override ErrorCode ErrorCode => ErrorCode.PerfilInvalido;

        public override string Message => "El perfil ingresado no es válido";
    }
}