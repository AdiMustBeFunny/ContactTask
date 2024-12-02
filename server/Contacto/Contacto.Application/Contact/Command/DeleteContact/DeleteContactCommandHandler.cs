using Contacto.Domain.Abstractions;
using Contacto.Domain.Abstractions.EntityService;
using Contacto.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Contacto.Application.Contact.Command.DeleteContact;

internal sealed class DeleteContactCommandHandler : ICommandHandler<DeleteContactCommand, Result>
{
    private readonly IAppDbContext _appDbContext;

    public DeleteContactCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _appDbContext.Contacts.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (contact == null) 
        {
            return Result.Failure(ContactApplicationErrors.NoContactWithGivenIdentifier);
        }

        _appDbContext.Contacts.Remove(contact);
        await _appDbContext.SaveChangesAsync();

        return Result.Success();
    }
}
