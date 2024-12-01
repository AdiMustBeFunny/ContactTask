using Contacto.Domain.Abstractions;
using Contacto.Domain.Abstractions.EntityService;
using Contacto.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Contacto.Application.User.Command.AuthenticateUser;

internal sealed class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand, Result<AuthenticateUserResultDTO>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IApplicationUserService _applicationUserService;

    public AuthenticateUserCommandHandler(IAppDbContext appDbContext, IApplicationUserService applicationUserService)
    {
        _appDbContext = appDbContext;
        _applicationUserService = applicationUserService;
    }

    public async Task<Result<AuthenticateUserResultDTO>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Username == request.UserName);

        if (user == null) 
        {
            return Result.Failure<AuthenticateUserResultDTO>(UserApplicationErrors.NoUserWithGivenUsername);
        }

        var authenticateResult = _applicationUserService.AuthenticateUser(user, request.Password);

        if (authenticateResult.IsFailure) 
        {
            return Result.Failure<AuthenticateUserResultDTO>(authenticateResult.Error);
        }

        return new AuthenticateUserResultDTO(authenticateResult.Value);
    }
}
