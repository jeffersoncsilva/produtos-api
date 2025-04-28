using System.Text.Json.Serialization;

namespace FE.Application.Features.Sales.UpdateSaleCommand;

public class UpdateSaleResult
{
	[JsonPropertyName("id")]
	public Guid SaleIdUpdated { get; set; }
}
