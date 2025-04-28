using System.Text.Json.Serialization;

namespace FE.Application.Features.Sales.GetSaleByIdCommand;

public class GetSaleDetailByIdResponse
{
	[JsonPropertyName("sale_id")]
	public Guid Id { get; set; }

	[JsonPropertyName("observation")]
	public string? Observatin { get; set; }

	[JsonPropertyName("price")]
	public decimal Price { get; set; }

	[JsonPropertyName("descount")]
	public decimal Descount { get; set; }

	[JsonPropertyName("product_itens")]
	public List<SaleItemResponse> Itens { get; set; } = new();
}

public class SaleItemResponse
{
	[JsonPropertyName("product_id")]
	public Guid ProductId { get; set; }

	[JsonPropertyName("product_name")]
	public string? ProductName { get; set; }

	[JsonPropertyName("product_description")]
	public string? ProductDescription { get; set; }

	[JsonPropertyName("product_category")]
	public string? ProductCategory { get; set; }

	[JsonPropertyName("product_brand")]
	public string? ProductBrand { get; set; }

	[JsonPropertyName("product_image_url")]
	public string? ProductImageUrl { get; set; }

	[JsonPropertyName("quantity")]
	public int Quantity { get; set; }

	[JsonPropertyName("product_price")]
	public decimal ProductPrice { get; set; }
}
