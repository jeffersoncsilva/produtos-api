using System.Text.Json.Serialization;

namespace FE.Application.Features.Sales.CreateSaleCommand;

public class CreateSaleResponse
{
	[JsonPropertyName("id")]
	public Guid Id { get; set; }
}
