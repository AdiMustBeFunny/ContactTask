using Contacto.Domain.Abstractions;

namespace Contacto.Application.Contact.Query.GetContacts;

public record GetAllContactsQuery() : IQuery<IEnumerable<ContactListItemDTO>>;
