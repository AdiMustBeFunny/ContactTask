using Contacto.Domain.Abstractions;
using Contacto.Utilities;

namespace Contacto.Application.Contact.Query.GetContactById;

public record GetContactByIdQuery(Guid ContactId) : IQuery<Result<ContactDTO>>;

