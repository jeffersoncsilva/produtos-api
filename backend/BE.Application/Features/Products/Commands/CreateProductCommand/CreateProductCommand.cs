using MediatR;
using BE.Domain.Entities;

namespace BE.Application.Features.Products.Commands.CreateProductCommand;

public class CreateProductCommand : IRequest<CreateProductResult>
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public string? ImageUrl { get; init; }
    public string? Category { get; init; }
    public string? Brand { get; init; }
    public int Stock { get; init; }
    public bool IsActive { get; init; }
    public string? CreatedBy { get; init; }

    public Product ToEntity()
    {
        return new Product()
        {
            Name = Name,
            Description = Description,
            Price = Price,
            ImageUrl = ImageUrl,
            Category = Category,
            Brand = Brand,
            Stock = Stock,
            IsActive = IsActive,
            CreatedBy = CreatedBy,
            ModifiedBy = CreatedBy
        };
    }
}