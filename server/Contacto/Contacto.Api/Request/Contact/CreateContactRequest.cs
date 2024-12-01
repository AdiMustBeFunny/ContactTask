namespace Contacto.Api.Request.Contact;

public record CreateContactRequest(
        string Name,
        string Surname,
        string Email,
        string Password,
        string PhoneNumber,
        DateOnly? BirthDate,
        Guid? ContactCategoryId,
        Guid? ContactSubCategoryId,
        string? CustomContactCategory);
