using System.Text.Json.Serialization;
using MediatR;
using BE.Domain.Entities;

namespace BE.Application.Features.Products.Commands.UpdateProductCommand;

public class UpdateProductCommandRequest : IRequest<UpdateProductResult>
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

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

    [JsonPropertyName("modified_by")]
    public string? ModifiedBy { get; init; }

    public Product ToEntity()
    {
        return new Product()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            ImageUrl = ImageUrl,
            Category = Category,
            Brand = Brand,
            Stock = Stock,
            IsActive = IsActive,
            ModifiedBy = ModifiedBy
        };
    }
}