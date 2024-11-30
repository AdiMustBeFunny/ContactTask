using Contacto.Domain.Abstractions;
using Contacto.Domain.Entities;
using Contacto.Utilities;

namespace Contacto.Application.Contact.Command.CreateContact;

public record CreateContactCommand(
        string name,
        string surname,
        string email,
        string password,
        string phoneNumber,
        DateOnly? birthDate,
        Guid? contactCategoryId,
        Guid? contactSubCategoryId,
        string? customContactCategory) : ICommand<Result<CreateContactResultDTO>>;
