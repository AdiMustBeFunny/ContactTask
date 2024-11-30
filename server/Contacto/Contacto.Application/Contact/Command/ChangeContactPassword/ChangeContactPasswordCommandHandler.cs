using Contacto.Domain.Abstractions;
using Contacto.Domain.Abstractions.EntityService;
using Contacto.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Contacto.Application.Contact.Command.ChangeContactPassword
{
    internal sealed class ChangeContactPasswordCommandHandler : ICommandHandler<ChangeContactPasswordCommand, Result>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IPasswordService _passwordService;
        private readonly IContactEntityService _contactEntityService;

        public ChangeContactPasswordCommandHandler(IAppDbContext appDbContext, IPasswordService passwordService, IContactEntityService contactEntityService)
        {
            _appDbContext = appDbContext;
            _passwordService = passwordService;
            _contactEntityService = contactEntityService;
        }

        public async Task<Result> Handle(ChangeContactPasswordCommand request, CancellationToken cancellationToken)
        {
            var contact = await _appDbContext.Contacts.FirstOrDefaultAsync(c => c.Id == request.ContactId);

            if (contact == null) 
            {
                return Result.Failure(ContactApplicationErrors.NoContactWithGivenIdentifier);
            }

            var changePasswordResult = _contactEntityService.ChangePassword(contact, request.Password);

            if (changePasswordResult.IsFailure) 
            {
                return Result.Failure(changePasswordResult.Error);
            }

            await _appDbContext.SaveChangesAsync();

            return Result.Success();
        }
    }
}
