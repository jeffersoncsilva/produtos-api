using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Sales.Commands.CreateSaleCommand;
using RO.DevTest.Application.Features.Sales.Commands.GetSalesCommand;
using System.ComponentModel;
using RO.DevTest.Application.Features.Sales.Commands.GetSaleByIdCommand;
using RO.DevTest.Application.Features.Sales.Commands.UpdateSaleCommand;

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
    [EndpointName("Obtem lista de vendas.")]
    [EndpointSummary("Obtem uma lista de vendas informando a pagina e a quantidade de itens.")]
    public async Task<IActionResult> GetVendas([Description("Pagina de venda requerida.")][FromQuery] int page, [Description("Quantidade de vendas para aretornar.")][FromQuery] int pageSize)
    {
        var response = await mediator.Send(new GetSalesCommandRequest(page, pageSize));
        return Ok(response);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetSaleByIdCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetSaleByIdCommandResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(GetSaleByIdCommandResponse), StatusCodes.Status404NotFound)]
    [EndpointName("Obtem venda por Id.")]
    [EndpointSummary("Obtem uma venda informando o Id.")]
    public async Task<IActionResult> GetVendaById([FromRoute] Guid id)
    {
        var resutlado = await mediator.Send(new GetSaleByIdCommandRequest(id));
        if (resutlado is null)
            return NotFound();
        return Ok(resutlado);
    }
    
    
    [HttpPut]
    [ProducesResponseType(typeof(UpdateSaleCommandReponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UpdateSaleCommandReponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(UpdateSaleCommandReponse), StatusCodes.Status404NotFound)]
    [EndpointName("Atualiza uma venda.")]
    [EndpointSummary("Atualiza todos os itens de uma venda pelo informado.")]
    public async Task<IActionResult> UpdateVendaById([FromBody] UpdateSaleCommandRequest request)
    {
        var resutlado = await mediator.Send(request);
        if (resutlado is null)
            return NotFound();
        return Ok(resutlado);
    }
}