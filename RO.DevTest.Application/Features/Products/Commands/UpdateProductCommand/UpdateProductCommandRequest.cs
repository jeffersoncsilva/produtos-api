using MediatR;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Features.Products.Commands.UpdateProductCommand;

public class UpdateProductCommandRequest : IRequest<UpdateProductResult>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public string? ImageUrl { get; init; }
    public string? Category { get; init; }
    public string? Brand { get; init; }
    public int Stock { get; init; }
    public bool IsActive { get; init; }
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