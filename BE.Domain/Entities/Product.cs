using BE.Domain.Abstract;

namespace BE.Domain.Entities;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string? Category { get; set; }
    public string? Brand { get; set; }
    public int Stock { get; set; }
    public bool IsActive { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
}