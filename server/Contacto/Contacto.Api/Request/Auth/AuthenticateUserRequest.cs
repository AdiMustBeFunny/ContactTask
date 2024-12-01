namespace Contacto.Api.Request.Auth;

public record AuthenticateUserRequest(string UserName, string Password);