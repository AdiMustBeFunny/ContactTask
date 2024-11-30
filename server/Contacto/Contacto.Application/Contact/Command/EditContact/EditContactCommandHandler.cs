using Contacto.Application.Contact.Command.CreateContact;
using Contacto.Domain.Abstractions;
using Contacto.Domain.Abstractions.EntityService;
using Contacto.Domain.Entities;
using Contacto.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Contacto.Application.Contact.Command.EditContact;

internal sealed class EditContactCommandHandler : ICommandHandler<EditContactCommand, Result>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IPasswordService _passwordService;
    private readonly IContactEntityService _contactEntityService;

    public EditContactCommandHandler(IAppDbContext appDbContext, IPasswordService passwordService, IContactEntityService contactEntityService)
    {
        _appDbContext = appDbContext;
        _passwordService = passwordService;
        _contactEntityService = contactEntityService;
    }
    public async Task<Result> Handle(EditContactCommand request, CancellationToken cancellationToken)
    {
        bool emailIsUnique;
        ContactCategory? contactCategory = null;
        ContactSubCategory? contactSubCategory = null;
        var contact = await _appDbContext.Contacts.FirstOrDefaultAsync(c => c.Id == request.contactId);

        if (contact == null) 
        {
            return Result.Failure(ContactApplicationErrors.NoContactWithGivenIdentifier);
        }

        emailIsUnique = !await _appDbContext.Contacts.AnyAsync(c => c.Id != request.contactId && c.Email == request.email);

        if (request.contactCategoryId != null)
        {
            contactCategory = await _appDbContext.ContactCategories
                .Include(c => c.ContactSubCategories)
                .FirstOrDefaultAsync(c => c.Id == request.contactCategoryId);

            if (contactCategory == null)
            {
                return Result.Failure<CreateContactResultDTO>(ContactApplicationErrors.ContactCategoryDoesntExist);
            }

            if (request.contactSubCategoryId != null)
            {
                contactSubCategory = contactCategory.ContactSubCategories.FirstOrDefault(sub => sub.Id == request.contactSubCategoryId);

                if (contactSubCategory == null)
                {
                    return Result.Failure<CreateContactResultDTO>(ContactApplicationErrors.ContactSubCategoryDoesntExist);
                }
            }
        }

        var editContactResult = _contactEntityService.EditContact(contact, request.name, request.surname, request.email, request.phoneNumber, request.birthDate, contactCategory, contactSubCategory, request.customContactCategory, emailIsUnique);

        if (editContactResult.IsFailure)
        {
            return Result.Failure<CreateContactResultDTO>(editContactResult.Error);
        }

        await _appDbContext.SaveChangesAsync();

        return Result.Success();
    }
}
