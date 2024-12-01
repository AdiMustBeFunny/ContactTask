using Contacto.Application.Contact;
using Contacto.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Contacto.Api.Controllers.Base;

public class BaseApiController : ControllerBase
{
    protected IActionResult MapResponse<TResponse>(Result<TResponse> result, int statusCode)
    {
        if (result.IsSuccess)
        {
            return StatusCode(statusCode, result.Value);
        }
        else
        {
            return HandleResultFailure(result);
        }
    }

    protected IActionResult MapResponse(Result result, int statusCode)
    {
        if (result.IsSuccess)
        {
            return StatusCode(statusCode);
        }
        else
        {
            return HandleResultFailure(result);
        }
    }

    protected virtual IActionResult HandleResultFailure(Result result)
    {
        return BadRequest(
                new ProblemDetails
                {
                    Title = "Error",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = result.Error.ErrorMessage
                });
    }
}
