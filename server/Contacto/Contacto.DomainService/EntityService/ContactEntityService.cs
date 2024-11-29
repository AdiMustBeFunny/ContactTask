using Contacto.Domain.Abstractions;
using Contacto.Domain.Abstractions.EntityService;
using Contacto.Domain.Entities;
using Contacto.Domain.Errors;
using Contacto.Utilities;

namespace Contacto.DomainService.EntityService;

public class ContactEntityService : IContactEntityService
{
    private readonly IPasswordService _passwordService;

    public ContactEntityService(IPasswordService passwordService)
    {
        _passwordService = passwordService;
    }

    public Result ChangeEmail(Contact contact, string newEmail, bool emailIsUnique)
    {
        if(contact == null)
        {
            throw new ArgumentNullException("Contact cannot be null");
        }

        if (!emailIsUnique)
        {
            return Result.Failure(ContactEntityServiceErrors.EmailAlreadyInUse);
        }

        if (string.IsNullOrWhiteSpace(newEmail)) 
        {
            return Result.Failure(ContactEntityServiceErrors.EmailCannotBeEmpty);
        }

        contact.Email = newEmail;

        return Result.Success();
    }

    public Result ChangePassword(Contact contact, string newPassword)
    {
        if (contact == null)
        {
            throw new ArgumentNullException("Contact cannot be null");
        }

        var passwordResult = _passwordService.CreatePassword(newPassword);

        if (passwordResult.IsFailure)
        {
            return Result.Failure(passwordResult.Error);
        }

        contact.PasswordHash = passwordResult.Value.PasswordHash;
        contact.PasswordSalt = passwordResult.Value.Salt;

        return Result.Success();
    }

    public Result<Contact> CreateContact(Guid id, string name, string surname, string email, string password, string phoneNumber, DateOnly? birthDate, ContactCategory? contactCategory, ContactSubCategory? contactSubCategory, string? customContactCategory, bool emailIsUnique)
    {
        if (id == Guid.Empty)
        {
            return Result.Failure<Contact>(GenericErrors.IdentifierWasEmpty);
        }

        var passwordResult = _passwordService.CreatePassword(password);

        if (passwordResult.IsFailure)
        {
            return Result.Failure<Contact>(passwordResult.Error);
        }

        var validateFieldsResult = ValidateCommonContactFields(name, surname, email, phoneNumber, birthDate, contactCategory, contactSubCategory, customContactCategory, emailIsUnique);

        if (validateFieldsResult.IsFailure) 
        {
            return Result.Failure<Contact>(validateFieldsResult.Error);
        }

        return new Contact(
            Guid.NewGuid(),
            name, 
            surname,
            email,
            passwordResult.Value.PasswordHash,
            passwordResult.Value.Salt,
            phoneNumber,
            birthDate,
            contactCategory,
            contactSubCategory,
            customContactCategory);
    }

    public Result EditContact(Contact contact, string name, string surname, string email, string phoneNumber, DateOnly? birthDate, ContactCategory? contactCategory, ContactSubCategory? contactSubCategory, string? customContactCategory, bool emailIsUnique)
    {
        if (contact == null)
        {
            throw new ArgumentNullException("Contact cannot be null");
        }

        var validateFieldsResult = ValidateCommonContactFields(name, surname, email, phoneNumber, birthDate, contactCategory, contactSubCategory, customContactCategory, emailIsUnique);

        if (validateFieldsResult.IsFailure)
        {
            return Result.Failure<Contact>(validateFieldsResult.Error);
        }

        contact.Name = name;
        contact.Surname = surname;
        contact.Email = email;
        contact.PhoneNumber = phoneNumber;
        contact.BirthDate = birthDate;
        contact.ContactCategory = contactCategory;
        contact.ContactSubCategory = contactSubCategory;
        contact.CustomContactCategory = customContactCategory;

        return Result.Success();
    }

    private Result ValidateCommonContactFields(string name, string surname, string email, string phoneNumber, DateOnly? birthDate, ContactCategory? contactCategory, ContactSubCategory? contactSubCategory, string? customContactCategory, bool emailIsUnique)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure(ContactEntityServiceErrors.NameEmpty);
        }

        if (string.IsNullOrWhiteSpace(surname))
        {
            return Result.Failure(ContactEntityServiceErrors.SurnameEmpty);
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            return Result.Failure(ContactEntityServiceErrors.EmailCannotBeEmpty);
        }

        if (!emailIsUnique)
        {
            return Result.Failure(ContactEntityServiceErrors.EmailAlreadyInUse);
        }

        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return Result.Failure(ContactEntityServiceErrors.PhoneNumberEmpty);
        }

        if (contactSubCategory != null && contactCategory == null)
        {
            return Result.Failure(ContactEntityServiceErrors.ContactCategoryCannotBeEmptyIfContactSubcategoryIsPresent);
        }

        if (!string.IsNullOrWhiteSpace(customContactCategory) && contactCategory == null)
        {
            return Result.Failure(ContactEntityServiceErrors.ContactCategoryCannotBeEmptyIfCustomContactSubcategoryIsPresent);
        }

        return Result.Success();
    }
}