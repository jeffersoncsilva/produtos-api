using System.Text.Json.Serialization;

namespace FE.Application.Features.Products.UpdateProductCommand;

public class UpdateProductCommandResponse
{
	[JsonPropertyName("id")]
	public Guid Id { get; set; }
}
