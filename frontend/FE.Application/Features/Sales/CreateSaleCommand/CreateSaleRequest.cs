using MediatR;
using System.Text.Json.Serialization;
using FE.ViewModels;

namespace FE.Application.Features.Sales.CreateSaleCommand;

public class CreateSaleRequest : IRequest<BaseResponse<CreateSaleResponse?>>
{
	[JsonPropertyName("observations")]
	public string Observation { get; set; } = string.Empty;
	[JsonPropertyName("price")]
	public decimal Price { get; set; }
	[JsonPropertyName("descount")]
	public decimal Descount { get; set; }
	[JsonPropertyName("itens")]
	public ICollection<ProductsSale> Itens { get; set; } = [];
}

public sealed class ProductsSale
{
	[JsonPropertyName("product_id")]
	public Guid Id { get; set; }
	[JsonPropertyName("quantity")]
	public int Quantity { get; set; }
}

