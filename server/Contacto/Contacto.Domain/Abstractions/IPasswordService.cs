using Contacto.Utilities;

namespace Contacto.Domain.Abstractions;

public interface IPasswordService
{
    Result<PasswordHashResult> CreatePassword(string password);

    Result VerifyPassword(string enteredPassword, string passwordHash, string passwordSalt);
}

public record PasswordHashResult(string PasswordHash, string Salt);
