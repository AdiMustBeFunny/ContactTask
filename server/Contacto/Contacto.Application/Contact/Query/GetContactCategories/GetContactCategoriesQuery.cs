using Contacto.Domain.Abstractions;

namespace Contacto.Application.Contact.Query.GetContactCategories;

public record GetContactCategoriesQuery() : IQuery<IEnumerable<ContactCategoryDTO>>;
