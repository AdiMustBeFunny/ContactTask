﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Contacto.Api.Utility;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "exception occurred: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server error",
            Detail = "Server error",
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
        .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}