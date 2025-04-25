namespace RO.DevTest.Application.Features.Products.Commands.CreateProductCommand;

public class CreateProductResult
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public string? CreatedBy { get; init; }
}