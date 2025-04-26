using System.Text.Json.Serialization;

namespace RO.DevTest.FronEnd.Application.Features.Products.UpdateProductCommand;

public class UpdateProductCommandResponse
{
	[JsonPropertyName("id")]
	public Guid Id { get; set; }
}
