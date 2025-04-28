using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Products.Commands.CreateProductCommand;
using RO.DevTest.Application.Features.Products.Commands.DeleteProductCommand;
using RO.DevTest.Application.Features.Products.Commands.GetProductByIdCommand;
using RO.DevTest.Application.Features.Products.Commands.GetProductsCommand;
using RO.DevTest.Application.Features.Products.Commands.UpdateProductCommand;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/[controller]")]
[OpenApiTags("Products")]
[ApiController]
[Authorize]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetProductsRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetProductsRequest), StatusCodes.Status401Unauthorized)]
    [EndpointSummary("Obtem produtos especificando a página e a quantidade de produtos.")]
    [EndpointName("Obter Página Especificada de Produtos")]
    public async Task<IActionResult> GetProducts([Description("Página a ser recuperada")][FromQuery] int page, [Description("Quantidade de página na página recuperada.")][FromQuery] int pageSize)
    {
        var products = await mediator.Send(new GetProductsRequest(page, pageSize));
        return Ok(products);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetProductByIdResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetProductByIdResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetProductByIdResult), StatusCodes.Status401Unauthorized)]
    [EndpointName("Obtém Produto por Id.")]
    [EndpointSummary("Obtém todos os dados de um produto pelo Id informado.")]
    public async Task<IActionResult> GetProductById([Description("Id do tipo GUID do produto.")][FromRoute] Guid id)
    {
        var product = await mediator.Send(new GetProductByIdCommand(id));
        //return product is null ? NotFound() : Ok(product);
        return Ok(product);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status401Unauthorized)]
    [EndpointName("Cria um produto.")]
    [EndpointSummary("Cria um produto com os dados informados.")]
    public async Task<IActionResult> CreateProduct([Description("Dados do produto informado.")][FromBody] CreateProductCommand command)
    {
        var product = await mediator.Send(command);
        return Created($"{HttpContext.Request.Path}/{product.Id}", product);
    }
    
    [HttpPut]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status401Unauthorized)]
    [EndpointName("Atualiza um produto.")]
    [EndpointSummary("Atualiza um produto com todos os dados informados.")]
    public async Task<IActionResult> UpdateProduct([Description("Dados para atualizar.")][FromBody]  UpdateProductCommandRequest command)
    {
        var product = await mediator.Send(command);
        return Ok(product);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status401Unauthorized)]
    [EndpointName("Remove um produto.")]
    [EndpointSummary("Remove um produto do banco de dados pelo Id informado. ")]
    public async Task<IActionResult> DeleteProduct([Description("Id do tipo GUID do produto.")][FromRoute] Guid id)
    {
        await mediator.Send(new DeleteProductCommandRequest(id));
        return NoContent();
    }
}