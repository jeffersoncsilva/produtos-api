using MediatR;
using System.Text.Json.Serialization;

namespace FE.Application.Features.Sales.UpdateSaleCommand;

public class UpdateSaleRequest : IRequest<UpdateSaleResult?>
{
	[JsonPropertyName("sale_id")]
	public Guid Id { get; set; }

	[JsonPropertyName("observation")]
	public string? Observatin { get; set; }

	[JsonPropertyName("price")]
	public decimal Price { get; set; }

	[JsonPropertyName("descount")]
	public decimal Descount { get; set; }

	[JsonPropertyName("product_itens")]
	public List<SaleItemUpdate> Itens { get; set; } = new();
}


public class SaleItemUpdate
{
	[JsonPropertyName("product_id")]
	public Guid Id { get; set; }

	[JsonPropertyName("quantity")]
	public int Quantity { get; set; }
	[JsonIgnore]
	public string? ProductName { get; set; }
	[JsonIgnore]
	public decimal ProductPrice { get; set; }
}

