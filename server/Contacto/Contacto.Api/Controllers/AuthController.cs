using Contacto.Api.Controllers.Base;
using Contacto.Api.Request.Auth;
using Contacto.Application.User.Command.AuthenticateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contacto.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : BaseApiController
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AuthenticateUserResultDTO),StatusCodes.Status200OK)]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserRequest request)
    {
        var command = new AuthenticateUserCommand(request.UserName, request.Password);
        var result = await _mediator.Send(command);
        return MapResponse(result, StatusCodes.Status200OK);
    }
}
