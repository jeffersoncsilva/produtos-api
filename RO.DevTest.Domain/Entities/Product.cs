using RO.DevTest.Domain.Abstract;

namespace RO.DevTest.Domain.Entities;

public class Product : BaseEntity
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
    public string? ModifiedBy { get; init; }
}