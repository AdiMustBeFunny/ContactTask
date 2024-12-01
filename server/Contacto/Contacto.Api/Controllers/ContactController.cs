using Contacto.Api.Controllers.Base;
using Contacto.Api.Request.Contact;
using Contacto.Application.Contact;
using Contacto.Application.Contact.Command.ChangeContactPassword;
using Contacto.Application.Contact.Command.CreateContact;
using Contacto.Application.Contact.Command.EditContact;
using Contacto.Application.Contact.Query.GetContactById;
using Contacto.Application.Contact.Query.GetContactCategories;
using Contacto.Application.Contact.Query.GetContacts;
using Contacto.Application.User.Command.AuthenticateUser;
using Contacto.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contacto.Api.Controllers;

[ApiController]
[Route("contact")]
public class ContactController : BaseApiController
{
    private readonly IMediator _mediator;

    public ContactController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContactListItemDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContacts()
    {
        var query = new GetAllContactsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{contactId}")]
    [ProducesResponseType(typeof(ContactDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContactById(Guid contactId)
    {
        var query = new GetContactByIdQuery(contactId);
        var result = await _mediator.Send(query);
        return MapResponse(result, StatusCodes.Status200OK);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(CreateContactResultDTO), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateContact([FromBody] CreateContactRequest request)
    {
        var command = new CreateContactCommand(request.Name, request.Surname, request.Email, request.Password, request.PhoneNumber, request.BirthDate, request.ContactCategoryId, request.ContactSubCategoryId, request.CustomContactCategory);
        var result = await _mediator.Send(command);
        return MapResponse(result, StatusCodes.Status201Created);
    }

    [Authorize]
    [HttpPost("{contactId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditContact([FromBody] EditContactRequest request, Guid contactId)
    {
        var command = new EditContactCommand(contactId, request.Name, request.Surname, request.Email, request.PhoneNumber, request.BirthDate, request.ContactCategoryId, request.ContactSubCategoryId, request.CustomContactCategory);
        var result = await _mediator.Send(command);
        return MapResponse(result, StatusCodes.Status204NoContent);
    }

    [Authorize]
    [HttpPost("{contactId}/password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangeContactPassword([FromBody] ChangeContactPasswordRequest request, Guid contactId)
    {
        var command = new ChangeContactPasswordCommand(contactId, request.Password);
        var result = await _mediator.Send(command);
        return MapResponse(result, StatusCodes.Status204NoContent);
    }

    [HttpGet("category")]
    [ProducesResponseType(typeof(IEnumerable<ContactCategoryDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContactCategories()
    {
        var query = new GetContactCategoriesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    protected override IActionResult HandleResultFailure(Result result)
    {
        return result switch
        {
            { Error: Error error } when error.ErrorCode == ContactApplicationErrors.NoContactWithGivenIdentifierId => NotFound(
                new ProblemDetails
                {
                    Title = "Error",
                    Status = StatusCodes.Status404NotFound,
                    Detail = result.Error.ErrorMessage
                }),
            _ => BadRequest(
                new ProblemDetails
                {
                    Title = "Error",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = result.Error.ErrorMessage
                })
        };
    }
}
