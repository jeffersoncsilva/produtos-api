using System.Text.Json.Serialization;

namespace RO.DevTest.FronEnd.Application.Features.Sales.CreateSaleCommand;

public class CreateSaleResponse
{
	[JsonPropertyName("id")]
	public Guid Id { get; set; }
}
