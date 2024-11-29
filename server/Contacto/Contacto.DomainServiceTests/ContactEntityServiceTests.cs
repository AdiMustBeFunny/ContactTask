using Contacto.Domain.Abstractions;
using Contacto.Domain.Abstractions.EntityService;
using Contacto.Domain.Entities;
using Contacto.DomainService;
using Contacto.DomainService.EntityService;

namespace Contacto.DomainServiceTests;

public class ContactEntityServiceTests
{
    private readonly IContactEntityService _contactEntityService;
    private readonly IPasswordService _passwordService;

    public ContactEntityServiceTests()
    {
        _passwordService = new PasswordService();
        _contactEntityService = new ContactEntityService(_passwordService);
    }

    [Fact]
    public void Test1()
    {
        //var contact = new Contact(Guid.NewGuid(),"Mike","Smith","mike@example.com", "hash", "salt", "123456789", null, null, null, null);
        var id = Guid.NewGuid();
        var name = "Mike";
        var surname = "Smith";
        var email = "mike@example.com";
        var password = "Admin123.";
        var phoneNumber = "123456789";
        DateOnly? birthDate = null;
        ContactCategory? category = null;
        ContactSubCategory? subCategory = null;
        string? customSubCategory = null;
        bool emailUnique = true;

        var createResult = _contactEntityService.CreateContact(id, name, surname, email, password, phoneNumber, birthDate, category, subCategory, customSubCategory, emailUnique);
    
        Assert.True(createResult.IsSuccess);
    }

    [Fact]
    public void Test2()
    {
        var contact = new Contact(Guid.NewGuid(),"Mike","Smith","mike@example.com", "hash", "salt", "123456789", null, null, null, null);
        var id = Guid.NewGuid();
        var name = "John";
        var surname = "Doe";
        var email = "john@example.com";
        var phoneNumber = "333333";
        DateOnly? birthDate = null;
        ContactCategory? category = null;
        ContactSubCategory? subCategory = null;
        string? customSubCategory = null;
        bool emailUnique = true;

        var createResult = _contactEntityService.EditContact(contact, name, surname, email, phoneNumber, birthDate, category, subCategory, customSubCategory, emailUnique);

        Assert.True(createResult.IsSuccess);
    }


}