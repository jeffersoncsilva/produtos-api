
using System.Text.Json.Serialization;

namespace FE.Application.Features.Products.CreateProductCommand;

public sealed class CreateProductResponse
{
	[JsonPropertyName("id")]
	public Guid Id { get; init; }
	[JsonPropertyName("name")]
	public string? Name { get; init; }
	[JsonPropertyName("description")]
	public string? Description { get; init; }
	[JsonPropertyName("price")]
	public decimal Price { get; init; }
	[JsonPropertyName("created_by")]
	public string? CreatedBy { get; init; }
}
