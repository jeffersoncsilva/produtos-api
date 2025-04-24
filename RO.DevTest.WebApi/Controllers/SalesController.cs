using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Sales.Commands.CreateSaleCommand;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/sales")]
[OpenApiTags("Sales")]
public class SalesController(IMediator mediator) : Controller
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateVenda(CreateSaleCommand request)
    {
        CreateSaleResult response = await mediator.Send(request);
        return Created(HttpContext.Request.GetDisplayUrl(), response);
    }
}