using FE.ViewModels.Features.Product;
using System.Text.Json.Serialization;

namespace FE.Application.Features.Products.GetProductsCommand;

public class GetProductResponse
{
	[JsonPropertyName("page")]
	public int Page { get; init; }
	[JsonPropertyName("size")]
	public int Size { get; init; }
	[JsonPropertyName("products")]
	public IEnumerable<ProductSimpleViewModel> Products { get; init; }
}
