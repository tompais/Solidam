using System;
using Enums;

namespace Exceptions
{
    public class IdNoValidoException : SolidamException
    {
        public override ErrorCode ErrorCode => ErrorCode.IdNoValido;
        private Type EntityType { get; }
        private ulong Id { get; }
        public override string Message => $"El Id {Id} de la entidad {EntityType.Name} no es válido";

        public IdNoValidoException(Type entityType, ulong id)
        {
            EntityType = entityType;
            Id = id;
        }
    }
}