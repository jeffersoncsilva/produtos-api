using System.Text.Json.Serialization;

namespace BE.Application.Features.Products.Commands.UpdateProductCommand;

public class UpdateProductResult(Guid id)
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; } = id;
}