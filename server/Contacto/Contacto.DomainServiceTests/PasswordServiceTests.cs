using Contacto.Domain.Abstractions.EntityService;
using Contacto.Domain.Abstractions;
using Contacto.DomainService.EntityService;
using Contacto.DomainService;

namespace Contacto.DomainServiceTests;

public class PasswordServiceTests
{
    private readonly IPasswordService _passwordService;

    public PasswordServiceTests()
    {
        _passwordService = new PasswordService();
    }

    [Fact]
    public void GivenCorrectHashAndSalt_VerifyPassword_ShouldVerifySuccessfully()
    {
        var password = "Admin123.";
        var hash = "1lrTpGzaEwmM4CQZADRN1mc91EpB77u9bCj/grHiy5nwI3jdHAJbppSfZpbFYqxk";
        var salt = "1lrTpGzaEwmM4CQZADRN1g==";

        var passwordVerifyResult = _passwordService.VerifyPassword(password, hash, salt);

        Assert.True(passwordVerifyResult.IsSuccess);
    }
}
