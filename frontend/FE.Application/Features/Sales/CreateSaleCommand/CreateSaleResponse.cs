using System.Text.Json.Serialization;

namespace FE.Application.Features.Sales.CreateSaleCommand;

public class CreateSaleResponse
{
	[JsonPropertyName("id")]
	public Guid SaleId { get; set; }

	[JsonPropertyName("success")]
	public bool Success { get; set; }

	[JsonPropertyName("errors")]
	public List<string>? Errors { get; set; }
}
