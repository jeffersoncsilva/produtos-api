using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Sales.Commands.CreateSaleCommand;
using RO.DevTest.Application.Features.Sales.Commands.GetSalesCommand;
using System.ComponentModel;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/[controller]")]
[OpenApiTags("Sales")]
[ApiController]
public class SalesController(IMediator mediator) : Controller
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status401Unauthorized)]
    [EndpointName("Cria uma venda.")]
	[EndpointSummary("Cria uma venda com os produtos informados.")]
	public async Task<IActionResult> CreateVenda(CreateSaleCommand request)
    {
        CreateSaleResult response = await mediator.Send(request);
        return Created(HttpContext.Request.GetDisplayUrl(), response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetSalesResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetSalesResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetVendas([Description("Pagina de venda requerida.")][FromQuery] int page, [Description("Quantidade de vendas para aretornar.")][FromQuery] int pageSize)
    {
        var response = await mediator.Send(new GetSalesCommandRequest(page, pageSize));
        return Ok(response);
    }
}