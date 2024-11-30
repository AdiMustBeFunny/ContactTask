using Contacto.Application.User;
using Contacto.Domain.Abstractions;
using Contacto.Domain.Abstractions.EntityService;
using Contacto.Domain.Entities;
using Contacto.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Contacto.Application.Contact.Command.CreateContact;

internal sealed class CreateContactCommandHandler : ICommandHandler<CreateContactCommand, Result<CreateContactResultDTO>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IPasswordService _passwordService;
    private readonly IContactEntityService _contactEntityService;

    public CreateContactCommandHandler(IAppDbContext appDbContext, IPasswordService passwordService, IContactEntityService contactEntityService)
    {
        _appDbContext = appDbContext;
        _passwordService = passwordService;
        _contactEntityService = contactEntityService;
    }

    public async Task<Result<CreateContactResultDTO>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        bool emailIsUnique;
        ContactCategory? contactCategory = null;
        ContactSubCategory? contactSubCategory = null;

        emailIsUnique = !await _appDbContext.Contacts.AnyAsync(c => c.Email == request.email);

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

        var createContactResult = _contactEntityService.CreateContact(Guid.NewGuid(), request.name, request.surname, request.email, request.password, request.phoneNumber, request.birthDate, contactCategory, contactSubCategory, request.customContactCategory, emailIsUnique);

        if (createContactResult.IsFailure)
        {
            return Result.Failure<CreateContactResultDTO>(createContactResult.Error);
        }

        var contact = createContactResult.Value;

        _appDbContext.Contacts.Add(contact);
        await _appDbContext.SaveChangesAsync();

        return new CreateContactResultDTO(createContactResult.Value.Id);
    }
}
