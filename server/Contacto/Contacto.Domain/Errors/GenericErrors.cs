using Contacto.Utilities;

namespace Contacto.Domain.Errors;

public static class GenericErrors
{
    public static readonly string IdentifierWasEmptyId = $"{nameof(GenericErrors)}.{nameof(IdentifierWasEmpty)}";
    public static readonly Error IdentifierWasEmpty = new(IdentifierWasEmptyId, "Id was empty");
}
