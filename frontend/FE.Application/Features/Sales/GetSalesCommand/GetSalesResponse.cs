using System.Text.Json.Serialization;

namespace FE.Application.Features.Sales.GetSalesCommand;

public class GetSalesResponse
{
	[JsonPropertyName("sales_item")]
	public IReadOnlyList<SaleItem> Sales { get; set; } = [];
	[JsonPropertyName("page")]
	public int Page { get; set; }
	[JsonPropertyName("size")]
	public int Size { get; set; }
}

public class SaleItem
{
	[JsonPropertyName("sale_id")]
	public Guid SaleId { get; set; }

	[JsonPropertyName("price")]
	public decimal Price { get; set; }

	[JsonPropertyName("descount")]
	public decimal Descount { get; set; }

	[JsonPropertyName("observation")]
	public string? Observation { get; set; }

	[JsonPropertyName("product_names")] public IList<string> ProductsName { get; set; } = [];
}
