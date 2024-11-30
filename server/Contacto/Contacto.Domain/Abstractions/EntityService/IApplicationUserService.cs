using Contacto.Domain.Entities;
using Contacto.Utilities;

namespace Contacto.Domain.Abstractions.EntityService;

public interface IApplicationUserService
{
    /// <summary>
    /// Function returns a jwt token as string
    /// </summary>
    Result<string> AuthenticateUser(ApplicationUser applicationUser, string password);
}
