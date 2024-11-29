namespace Contacto.Domain.Entities;

public class ApplicationUser
{
    private ApplicationUser() { }

    public ApplicationUser(Guid id, string username, string passwordHash, string passwordSalt)
    {
        Id = id;
        Username = username;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    public Guid Id { get; internal set; }

    public string Username { get; internal set; }

    public string PasswordHash { get; internal set; }

    public string PasswordSalt { get; internal set; }

    public static readonly ApplicationUser BuiltInUser = new(Guid.NewGuid(), "admin", "1lrTpGzaEwmM4CQZADRN1mc91EpB77u9bCj/grHiy5nwI3jdHAJbppSfZpbFYqxk", "1lrTpGzaEwmM4CQZADRN1g==");
}
