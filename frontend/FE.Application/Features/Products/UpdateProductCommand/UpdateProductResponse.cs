using System.Text.Json.Serialization;

namespace FE.Application.Features.Products.UpdateProductCommand;

public class UpdateProductResponse
{
	[JsonPropertyName("id")]
	public Guid Id { get; set; }
}
