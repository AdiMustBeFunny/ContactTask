using Contacto.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Contacto.Application.Contact.Query.GetContactCategories;

internal sealed class GetContactCategoriesQueryHandler : IQueryHandler<GetContactCategoriesQuery, IEnumerable<ContactCategoryDTO>>
{
    private readonly IAppDbContext _appDbContext;

    public GetContactCategoriesQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<ContactCategoryDTO>> Handle(GetContactCategoriesQuery request, CancellationToken cancellationToken)
    {
        var contactCategories = await _appDbContext.ContactCategories
            .Include(c => c.ContactSubCategories)
            .Select(c => new ContactCategoryDTO(
                c.Id,
                c.Title,
                c.CustomCategory,
                c.ContactSubCategories.Select(sub => new ContactSubCategoryDTO(sub.Id, sub.Title, sub.CategoryId))))
            .ToListAsync();

        return contactCategories;
    }
}
