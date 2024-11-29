using Contacto.Domain.Entities;
using Contacto.Utilities;

namespace Contacto.Domain.Abstractions.EntityService;

public interface IContactEntityService
{
    Result<Contact> CreateContact(
        Guid id,
        string name,
        string surname,
        string email,
        string password,
        string phoneNumber,
        DateOnly? birthDate,
        ContactCategory? contactCategory,
        ContactSubCategory? contactSubCategory,
        string? customContactCategory,
        bool emailIsUnique);

    Result EditContact(
        Contact contact,
        string name,
        string surname,
        string email,
        string phoneNumber,
        DateOnly? birthDate,
        ContactCategory? contactCategory,
        ContactSubCategory? contactSubCategory,
        string? customContactCategory,
        bool emailIsUnique);

    Result ChangeEmail(
        Contact contact,
        string newEmail,
        bool emailIsUnique);

    Result ChangePassword(
        Contact contact,
        string newPassword);
}
