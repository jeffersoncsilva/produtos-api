using System.Text.Json.Serialization;

namespace BE.Application.Features.Products.Commands.GetProductsCommand;

public class GetProductsResult
{
    [JsonPropertyName("products")]
    public IReadOnlyList<ProductsSimpleResult>? Products { get; init; }

    [JsonPropertyName("page")]
    public int Page { get; init; }
    
    [JsonPropertyName("size")]
    public int Size { get; init; }
}

public class ProductsSimpleResult
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
    
    [JsonPropertyName("name")]
    public string? Name { get; init; }
    
    [JsonPropertyName("price")]
    public decimal Price { get; init; }
	
    [JsonPropertyName("stock_quantity")]
	public int Stock { get; init; }
    
    [JsonPropertyName("description")]
	public string? Description { get; set; }
	
	[JsonPropertyName("is_active")]
	public bool IsActive { get; set; }
}

