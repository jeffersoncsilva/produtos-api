using RO.DevTest.Domain.Abstract;

namespace RO.DevTest.Domain.Entities;

public class Sale : BaseEntity
{
    public string Observation { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Descount { get; set; }
    public ICollection<SaleItem> Itens { get; set; } = [];
}