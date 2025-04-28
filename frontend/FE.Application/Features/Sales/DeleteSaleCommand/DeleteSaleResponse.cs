using System.Text.Json.Serialization;

namespace FE.Application.Features.Sales.DeleteSaleCommand;

public class DeleteSaleResponse
{
	[JsonPropertyName("success")]
	public bool Success { get; set; }
}
