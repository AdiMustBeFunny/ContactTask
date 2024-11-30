using Contacto.Domain.Abstractions;
using Contacto.Domain.Entities;
using Contacto.Utilities;

namespace Contacto.Application.Contact.Command.EditContact;

public record EditContactCommand(
        Guid contactId,
        string name,
        string surname,
        string email,
        string phoneNumber,
        DateOnly? birthDate,
        Guid? contactCategoryId,
        Guid? contactSubCategoryId,
        string? customContactCategory
    ) : ICommand<Result>;

