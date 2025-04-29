using System.Text.Json.Serialization;
using MediatR;

namespace BE.Application.Features.Sales.Commands.CreateSaleCommand;

public class CreateSaleRequest : IRequest<CreateSaleResult>
{
	[JsonPropertyName("name")]
	public string? Name { get; set; }
	
	[JsonPropertyName("observations")]
	public string Observation { get; set; } = string.Empty;
	
	[JsonPropertyName("price")]
	public decimal Price { get; set; }
	
	[JsonPropertyName("descount")]
	public decimal Descount { get; set; }
	
	[JsonPropertyName("itens")]
	public IEnumerable<ProductSaleCommand> Products { get; set; } = [];

	[JsonPropertyName("created_by")]
	public string? CreatedBy { get; set; }
}

public sealed class ProductSaleCommand
{
	[JsonPropertyName("product_id")]
	public Guid ProductId { get; set; }
	
	[JsonPropertyName("quantity")]
	public int Quantity { get; set; }
}

