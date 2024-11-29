using Contacto.Utilities;

namespace Contacto.Domain.Errors;

public static class PasswordServiceErrors
{
    public static readonly string PasswordsDontMatchId = $"{nameof(PasswordServiceErrors)}.{nameof(PasswordsDontMatch)}";
    public static readonly Error PasswordsDontMatch = new(PasswordsDontMatchId, "Incorrect Password");

    public static readonly string PasswordTooShortId = $"{nameof(PasswordServiceErrors)}.{nameof(PasswordTooShort)}";
    public static readonly Error PasswordTooShort = new(PasswordTooShortId, "Password too short");

    public static readonly string PasswordDoesntMeetCharacterRequirementsId = $"{nameof(PasswordServiceErrors)}.{nameof(PasswordTooShort)}";
    public static readonly Error PasswordDoesntMeetCharacterRequirements = new(PasswordDoesntMeetCharacterRequirementsId, "Password must contain at least one small letter, one capital letter, one digit, one special character");

    public static readonly string PasswordCannotBeEmptyId = $"{nameof(PasswordServiceErrors)}.{nameof(PasswordCannotBeEmpty)}";
    public static readonly Error PasswordCannotBeEmpty = new(PasswordCannotBeEmptyId, "Password cannot be empty");
}
