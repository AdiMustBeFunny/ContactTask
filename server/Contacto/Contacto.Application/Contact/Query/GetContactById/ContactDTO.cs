namespace Contacto.Application.Contact.Query.GetContactById;

public record ContactDTO(
    Guid Id,
    string Name,
    string Surname,
    string PhoneNumber,
    string Email,
    DateOnly? BirthDate,
    Guid? ContactCategoryId,
    Guid? ContactSubCategoryId,
    string? customContactCategory
    );
