using System;
using Enums;

namespace Exceptions
{
    public class IdNoValidoException : SolidamException
    {
        protected override ErrorCode ErrorCode => ErrorCode.IdNoValido;
        public Type EntityType { get; set; }
        public override string Message => $"El Id de la entidad {EntityType.Name} no es válido";

        public IdNoValidoException(Type entityType)
        {
            EntityType = entityType;
        }
    }
}