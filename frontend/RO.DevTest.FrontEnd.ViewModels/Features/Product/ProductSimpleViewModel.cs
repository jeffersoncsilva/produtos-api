using System.Text.Json.Serialization;

namespace RO.DevTest.FrontEnd.ViewModels.Features.Product;

public class ProductSimpleViewModel
{
	[JsonPropertyName("id")]
	public Guid Id { get; init; }
	[JsonPropertyName("name")]
	public string? Name { get; init; }
	[JsonPropertyName("price")]
	public decimal Price { get; init; }
}
