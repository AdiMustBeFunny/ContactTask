using Contacto.Domain.Abstractions;
using Contacto.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Contacto.Application.Contact.Query.GetContactById;

public class GetContactByIdQueryHandler : IQueryHandler<GetContactByIdQuery, Result<ContactDTO>>
{
    private readonly IAppDbContext _appDbContext;

    public GetContactByIdQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result<ContactDTO>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contact = await _appDbContext.Contacts
            .Include(c => c.ContactCategory)
            .Include(c => c.ContactSubCategory)
            .FirstOrDefaultAsync(c => c.Id == request.ContactId);

        if (contact == null) 
        {
            return Result.Failure<ContactDTO>(ContactApplicationErrors.NoContactWithGivenIdentifier);
        }

        return new ContactDTO(
            contact.Id,
            contact.Name,
            contact.Surname,
            contact.PhoneNumber,
            contact.Email,
            contact.BirthDate,
            contact.ContactCategory?.Id,
            contact.ContactSubCategory?.Id,
            contact.CustomContactCategory);
    }
}
