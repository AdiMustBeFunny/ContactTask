using Contacto.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Contacto.Application.Contact.Query.GetContacts;

internal sealed class GetAllContactsQueryHandler : IQueryHandler<GetAllContactsQuery, IEnumerable<ContactListItemDTO>>
{
    private readonly IAppDbContext _appDbContext;

    public GetAllContactsQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<ContactListItemDTO>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
    {
        var contacts = await _appDbContext.Contacts
            .Select(c => new ContactListItemDTO(c.Id, c.Name, c.Surname, c.PhoneNumber))
            .ToListAsync();

        return contacts;
    }
}
