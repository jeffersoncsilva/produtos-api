using System.Text.Json.Serialization;

namespace RO.DevTest.FronEnd.Application.Features.Sales.UpdateSaleCommand;

public class UpdateSaleResult
{
	[JsonPropertyName("id")]
	public Guid SaleIdUpdated { get; set; }
}
