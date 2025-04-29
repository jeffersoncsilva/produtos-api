using MediatR;
using System.Text.Json.Serialization;
using FE.ViewModels;

namespace FE.Application.Features.Sales.UpdateSaleCommand;

public class UpdateSaleRequest : IRequest<BaseResponse<UpdateSaleResult?>>
{
	[JsonPropertyName("sale_id")]
	public Guid Id { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	[JsonPropertyName("observation")]
	public string? Observation { get; set; }

	[JsonPropertyName("price")]
	public decimal Price { get; set; }

	[JsonPropertyName("descount")]
	public decimal Descount { get; set; }

	[JsonPropertyName("product_itens")]
	public List<SaleItemUpdate> Itens { get; set; } = new();

	[JsonPropertyName("modified_by")]
	public string? ModifiedBy { get; set; }
}


public class SaleItemUpdate
{
	[JsonPropertyName("product_id")]
	public Guid Id { get; set; }

	[JsonPropertyName("quantity")]
	public int Quantity { get; set; }
	[JsonIgnore]
	public string? ProductName { get; set; }
	[JsonIgnore]
	public decimal ProductPrice { get; set; }
}

