namespace Contacto.Domain.Entities;

public class Contact
{
    private Contact() { }

    internal Contact(Guid id, string name, string surname, string email, string passwordHash, string passwordSalt, string phoneNumber, DateOnly? birthDate, ContactCategory? contactCategory, ContactSubCategory? contactSubCategory, string? customContactCategory)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        ContactCategory = contactCategory;
        ContactSubCategory = contactSubCategory;
        CustomContactCategory = customContactCategory;
    }

    public Guid Id { get; internal init; }

    public string Name { get; internal set; }

    public string Surname { get; internal set; }

    public string Email { get; internal set; }

    public string PasswordHash { get; internal set; }

    public string PasswordSalt { get; internal set; }

    public string PhoneNumber { get; internal set; }

    public DateOnly? BirthDate { get; internal set; }

    public ContactCategory? ContactCategory { get; internal set; }

    public ContactSubCategory? ContactSubCategory { get; internal set; }

    public string? CustomContactCategory { get; internal set; }
}
