using Contacto.Utilities;

namespace Contacto.Application.User;

public static class UserApplicationErrors
{
    public static readonly string NoUserWithGivenUsernameId = $"{nameof(UserApplicationErrors)}.{nameof(NoUserWithGivenUsername)}";
    public static readonly Error NoUserWithGivenUsername = new(NoUserWithGivenUsernameId, "Incorrect username or password");
}
