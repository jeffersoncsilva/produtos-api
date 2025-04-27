using System.Text.Json.Serialization;

namespace RO.DevTest.FronEnd.Application.Features.Sales.DeleteSaleCommand;

public class DeleteSaleResponse
{
	[JsonPropertyName("success")]
	public bool Success { get; set; }
}
