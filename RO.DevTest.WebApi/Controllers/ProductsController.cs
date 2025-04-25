using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Products.Commands.CreateProductCommand;
using RO.DevTest.Application.Features.Products.Commands.DeleteProductCommand;
using RO.DevTest.Application.Features.Products.Commands.GetProductByIdCommand;
using RO.DevTest.Application.Features.Products.Commands.GetProductsCommand;

namespace RO.DevTest.WebApi.Controllers;

[Microsoft.AspNetCore.Components.Route("api/products")]
[OpenApiTags("Products")]
[Authorize]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetProductsRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetProductsRequest), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetProducts([FromQuery] int page, [FromQuery] int pageSize)
    {
        var products = await mediator.Send(new GetProductsRequest(page, pageSize));
        return Ok(products);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetProductByIdResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetProductByIdResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetProductByIdResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetProductById([FromRoute] Guid id)
    {
        var product = await mediator.Send(new GetProductByIdCommand(id));
        //return product is null ? NotFound() : Ok(product);
        return Ok(product);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var product = await mediator.Send(command);
        return Created($"{HttpContext.Request.Path}/{product.Id}", product);
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateProduct([FromBody] CreateProductCommand command)
    {
        var product = await mediator.Send(command);
        return Ok(product);
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteProductCommandRequest(id));
        return NoContent();
    }
}