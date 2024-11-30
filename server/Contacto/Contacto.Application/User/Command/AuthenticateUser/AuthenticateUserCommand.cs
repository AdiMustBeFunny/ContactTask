using Contacto.Utilities;
using Contacto.Domain.Abstractions;
namespace Contacto.Application.User.Command.AuthenticateUser;

public record AuthenticateUserCommand(string UserName, string Password) : ICommand<Result<AuthenticateUserResultDTO>>;
