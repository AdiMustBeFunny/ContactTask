namespace Contacto.Application.Contact.Query.GetContactCategories;

public record ContactCategoryDTO(Guid Id, string Title, bool CustomCategory, IEnumerable<ContactSubCategoryDTO> SubCategories);
