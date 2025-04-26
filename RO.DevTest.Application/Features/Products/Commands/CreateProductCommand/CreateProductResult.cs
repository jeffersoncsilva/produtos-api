using System.Text.Json.Serialization;

namespace RO.DevTest.Application.Features.Products.Commands.CreateProductCommand;

public class CreateProductResult
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
    [JsonPropertyName("name")]
    public string? Name { get; init; }
    [JsonPropertyName("description")]
    public string? Description { get; init; }
    [JsonPropertyName("price")]
    public decimal Price { get; init; }
    [JsonPropertyName("created_by")]
    public string? CreatedBy { get; init; }
}