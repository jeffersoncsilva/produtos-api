using System.ComponentModel;
using BE.Application.Features.Auth.Commands.LoginCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace BE.Api.Controllers;

[Route("api/auth")]
[OpenApiTags("Auth")]
[ApiController]
public class AuthController(IMediator _mediator) : ControllerBase {
    
    [HttpPost]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status400BadRequest)]
    [EndpointSummary("Realiza o login baseado no usuário e senha informados.")]
    [EndpointName("Realiza login")]
    public async Task<IActionResult> GetProducts([Description("Página a ser recuperada")][FromBody] LoginCommand request)
    {
        var result = await _mediator.Send(request);
        if (result is null)
            return BadRequest();
        
        if (!result.Success)
            return Unauthorized();

        return Ok(result);
    }
}
