using System.Text.Json.Serialization;

namespace RO.DevTest.Application.Features.Products.Commands.GetProductByIdCommand;

public class GetProductByIdResult
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
    
    [JsonPropertyName("created_on")]
    public DateTime CreatedOn { get; init; }
	
    [JsonPropertyName("modified_on")]
	public DateTime ModifiedOn { get; init; }
    
    [JsonPropertyName("name")]
    public string? Name { get; init; }
    
    [JsonPropertyName("description")]
    public string? Description { get; init; }
    
    [JsonPropertyName("price")]
    public decimal Price { get; init; }
    
    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; init; }

    [JsonPropertyName("category")]
    public string? Category { get; init; }

    [JsonPropertyName("brand")]
    public string? Brand { get; init; }

    [JsonPropertyName("stock")]
    public int Stock { get; init; }

    [JsonPropertyName("is_active")]
    public bool IsActive { get; init; }

    [JsonPropertyName("created_by")]
    public string? CreatedBy { get; init; }

	[JsonPropertyName("modified_by")]
	public string? ModifiedBy { get; init; }
}