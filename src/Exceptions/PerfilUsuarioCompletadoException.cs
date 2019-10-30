using Enums;

namespace Exceptions
{
    public class PerfilUsuarioCompletadoException : SolidamException
    {
        public override ErrorCode ErrorCode => ErrorCode.PerfilUsuarioCompletado;
        public override string Message => "El perfil del usuario ya se encuentra completo";
    }
}