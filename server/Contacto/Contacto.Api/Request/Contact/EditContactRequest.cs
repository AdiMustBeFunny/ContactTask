namespace Contacto.Api.Request.Contact;

public record EditContactRequest(
        string Name,
        string Surname,
        string Email,
        string PhoneNumber,
        DateOnly? BirthDate,
        Guid? ContactCategoryId,
        Guid? ContactSubCategoryId,
        string? CustomContactCategory);
