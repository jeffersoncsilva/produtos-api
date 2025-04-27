using System.Text.Json.Serialization;
using MediatR;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Features.Sales.Commands.CreateSaleCommand;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
	[JsonPropertyName("observations")]
	public string Observation { get; set; } = string.Empty;
	
	[JsonPropertyName("price")]
	public decimal Price { get; set; }
	
	[JsonPropertyName("descount")]
	public decimal Descount { get; set; }
	
	[JsonPropertyName("itens")]
	public IEnumerable<ProductSaleCommand> Products { get; set; } = [];
}

public sealed class ProductSaleCommand
{
	[JsonPropertyName("product_id")]
	public Guid ProductId { get; set; }
	
	[JsonPropertyName("quantity")]
	public int Quantity { get; set; }
}

