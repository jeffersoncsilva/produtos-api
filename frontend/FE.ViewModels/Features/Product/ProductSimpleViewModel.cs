using System.Text.Json.Serialization;

namespace FE.ViewModels.Features.Product;

public class ProductSimpleViewModel
{
	[JsonPropertyName("id")]
	public Guid Id { get; init; }
	[JsonPropertyName("name")]
	public string? Name { get; init; }
	[JsonPropertyName("price")]
	public decimal Price { get; init; }
	[JsonPropertyName("stock_quantity")]
	public int Stock { get; init; }
}
