using System.Text.Json.Serialization;

namespace BE.Application.Features.Sales.Commands.GetSalesCommand;
public class GetSalesResult
{
	[JsonPropertyName("sales_item")]
	public IReadOnlyList<SaleItem> Sales { get; set; } = [];
	
	[JsonPropertyName("page")]
	public int Page { get; set; }
	
	[JsonPropertyName("size")]
	public int Size { get; set; }
	
	[JsonPropertyName("total_sales")]
	public int TotalSales { get; set; }
}

public class SaleItem
{
	[JsonPropertyName("name")] 
	public string Name { get; set; } = string.Empty;
	
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
