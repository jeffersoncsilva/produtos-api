using BE.Application.Features.User.Commands.CreateUserCommand;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace BE.Api.Controllers;

[Route("api/user")]
[OpenApiTags("Users")]
[ApiController]
public class UsersController(IMediator mediator) : ControllerBase {
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(CreateUserResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateUserResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser(CreateUserCommand request) {
        CreateUserResult response = await _mediator.Send(request);
        return Created(HttpContext.Request.GetDisplayUrl(), response);
    }
}
