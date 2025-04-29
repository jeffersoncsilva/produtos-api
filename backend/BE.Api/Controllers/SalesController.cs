using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.ComponentModel;
using BE.Application.Features.Sales.Commands.CreateSaleCommand;
using BE.Application.Features.Sales.Commands.DeleteSaleCommand;
using BE.Application.Features.Sales.Commands.GetSaleByIdCommand;
using BE.Application.Features.Sales.Commands.GetSalesCommand;
using BE.Application.Features.Sales.Commands.UpdateSaleCommand;
using Microsoft.AspNetCore.Authorization;

namespace BE.Api.Controllers;

[Route("api/[controller]")]
[OpenApiTags("Sales")]
[ApiController]
public class SalesController(IMediator mediator) : Controller
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status401Unauthorized)]
    [EndpointName("Cria uma venda.")]
	[EndpointSummary("Cria uma venda com os produtos informados.")]
	public async Task<IActionResult> CreateVenda(CreateSaleRequest request)
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
    [Authorize(Roles = "Admin")]
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
    
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(DeleteSaleCommandResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(DeleteSaleCommandResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(DeleteSaleCommandResponse), StatusCodes.Status404NotFound)]
    [EndpointName("Deleta uma venda.")]
    [EndpointSummary("Deleta todos os itens de uma venda pelo informado.")]
    public async Task<IActionResult> DeleteVendaById([FromRoute] Guid id)
    {
        var resutlado = await mediator.Send(new DeleteSaleCommandRequest(id));
        if (resutlado is null || !resutlado.Success)
            return NotFound();
        return Ok(resutlado);
    }
}