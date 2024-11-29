using Contacto.Utilities;

namespace Contacto.DomainService.EntityService;

public static class ContactEntityServiceErrors
{
    public static readonly string NameEmptyId = $"{nameof(ContactEntityServiceErrors)}.{nameof(NameEmpty)}";
    public static readonly Error NameEmpty = new(NameEmptyId, "Contact's name cannot be empty");

    public static readonly string SurnameEmptyId = $"{nameof(ContactEntityServiceErrors)}.{nameof(SurnameEmpty)}";
    public static readonly Error SurnameEmpty = new(SurnameEmptyId, "Contact's surname cannot be empty");

    public static readonly string EmailAlreadyInUseId = $"{nameof(ContactEntityServiceErrors)}.{nameof(EmailAlreadyInUse)}";
    public static readonly Error EmailAlreadyInUse = new(EmailAlreadyInUseId, "There exists a contact with this email address");

    public static readonly string EmailCannotBeEmptyId = $"{nameof(ContactEntityServiceErrors)}.{nameof(EmailCannotBeEmpty)}";
    public static readonly Error EmailCannotBeEmpty = new(EmailCannotBeEmptyId, "Email cannot be empty");

    public static readonly string PhoneNumberEmptyId = $"{nameof(ContactEntityServiceErrors)}.{nameof(PhoneNumberEmpty)}";
    public static readonly Error PhoneNumberEmpty = new(PhoneNumberEmptyId, "Contact's phone number cannot be empty");

    public static readonly string ContactCategoryCannotBeEmptyIfContactSubcategoryIsPresentId = $"{nameof(ContactEntityServiceErrors)}.{nameof(ContactCategoryCannotBeEmptyIfContactSubcategoryIsPresent)}";
    public static readonly Error ContactCategoryCannotBeEmptyIfContactSubcategoryIsPresent = new(ContactCategoryCannotBeEmptyIfContactSubcategoryIsPresentId, "Contact's category cannot be empty if subcategory is present");

    public static readonly string ContactCategoryCannotBeEmptyIfCustomContactSubcategoryIsPresentId = $"{nameof(ContactEntityServiceErrors)}.{nameof(ContactCategoryCannotBeEmptyIfCustomContactSubcategoryIsPresent)}";
    public static readonly Error ContactCategoryCannotBeEmptyIfCustomContactSubcategoryIsPresent = new(ContactCategoryCannotBeEmptyIfCustomContactSubcategoryIsPresentId, "Contact's category cannot be empty if custom subcategory is present");

}