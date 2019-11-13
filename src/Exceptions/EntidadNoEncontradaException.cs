using System;
using Enums;

namespace Exceptions
{
    public class EntidadNoEncontradaException : SolidamException
    {
        public override ErrorCode ErrorCode => ErrorCode.EntidadNoEncontrada;
        private Type EntityType { get; }
        public override string Message => $"No se ha encontrado la entidad de tipo {EntityType.Name} deseada";

        public EntidadNoEncontradaException(Type entityType)
        {
            EntityType = entityType;
        }
    }
}