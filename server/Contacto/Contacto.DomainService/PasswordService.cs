using Contacto.Domain.Abstractions;
using Contacto.Domain.Errors;
using Contacto.Utilities;
using System.Security.Cryptography;
using System.Text;

namespace Contacto.DomainService;

public class PasswordService : IPasswordService
{
    private readonly HashSet<char> _capitalLetters = "ABCDEFGHIJKLMNOPRSTUQWXYZ".ToHashSet();
    private readonly HashSet<char> _smallLetters = "abcdefghijklmnoprstquwxyz".ToHashSet();
    private readonly HashSet<char> _specialCharacters = "!@#$%^&*()-=+_[]{};'\",./<>?".ToHashSet();
    private readonly HashSet<char> _digits = "1234567890".ToHashSet();
    private const int minimalPasswordLength = 8;

    public Result<PasswordHashResult> CreatePassword(string password)
    {
        Result passwordValidated = ValidateCharactersInPassword(password);

        if (passwordValidated.IsFailure)
        {
            return Result.Failure<PasswordHashResult>(passwordValidated.Error);
        }

        byte[] saltBytes = GenerateSalt();
        string hashedPassword = HashPassword(password, saltBytes);
        string base64Salt = Convert.ToBase64String(saltBytes);

        return new PasswordHashResult(hashedPassword, base64Salt);
    }

    public Result VerifyPassword(string enteredPassword, string passwordHash, string passwordSaltAsString)
    {
        var saltBytes = Convert.FromBase64String(passwordSaltAsString);

        byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);
        byte[] saltedPassword = new byte[enteredPasswordBytes.Length + saltBytes.Length];

        Buffer.BlockCopy(enteredPasswordBytes, 0, saltedPassword, 0, enteredPasswordBytes.Length);
        Buffer.BlockCopy(saltBytes, 0, saltedPassword, enteredPasswordBytes.Length, saltBytes.Length);

        string enteredPasswordHash = HashPassword(enteredPassword, saltBytes);

        if(enteredPasswordHash == passwordHash)
        {
            return Result.Success();
        }
        else
        {
            return Result.Failure(PasswordServiceErrors.PasswordsDontMatch);
        }
    }

    private Result ValidateCharactersInPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return Result.Failure(PasswordServiceErrors.PasswordCannotBeEmpty);
        }

        if (password.Length < minimalPasswordLength)
        {
            return Result.Failure(PasswordServiceErrors.PasswordTooShort);
        }

        bool capitalLettersPresent = false;
        bool smallLettersPresent = false;
        bool specialCharactersPresent = false;
        bool digitsPresent = false;

        foreach (char c in password) 
        {
            if (_capitalLetters.Contains(c))
            {
                capitalLettersPresent = true;
            }
            else if (_smallLetters.Contains(c)) 
            {
                smallLettersPresent = true;
            }
            else if (_specialCharacters.Contains(c))
            {
                specialCharactersPresent = true;
            }
            else if (_digits.Contains(c))
            {
                digitsPresent = true;
            }
        }

        if(!capitalLettersPresent || !smallLettersPresent || !specialCharactersPresent || !digitsPresent)
        {
            return Result.Failure(PasswordServiceErrors.PasswordDoesntMeetCharacterRequirements);
        }

        return Result.Success();
    }

    private string HashPassword(string password, byte[] salt)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

            byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
            Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
            Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

            return Convert.ToBase64String(hashedPasswordWithSalt);
        }
    }

    private byte[] GenerateSalt()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] salt = new byte[16];
            rng.GetBytes(salt);
            return salt;
        }
    }
}
