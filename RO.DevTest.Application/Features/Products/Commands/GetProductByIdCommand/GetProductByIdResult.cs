namespace RO.DevTest.Application.Features.Products.Commands.GetProductByIdCommand;

public class GetProductByIdResult
{
    public Guid Id { get; init; }
    public DateTime CreatedOn { get; init; }
    public DateTime ModifiedOn { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public string? ImageUrl { get; init; }
    public string? Category { get; init; }
    public string? Brand { get; init; }
    public int Stock { get; init; }
    public bool IsActive { get; init; }
    public string? CreatedBy { get; init; }
    public string? ModifiedBy { get; init; }
}