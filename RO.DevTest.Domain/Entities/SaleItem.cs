using RO.DevTest.Domain.Abstract;

namespace RO.DevTest.Domain.Entities;

public class SaleItem : BaseEntity
{
	public Guid ProductId { get; set; }
	public Product? Product { get; set; }
	public Sale? Sale { get; set; }
	public Guid SaleId { get; set; }
	public int Quantity { get; set; }
	public decimal Price { get; set; }

	public SaleItem(Product p, int qtd)
	{
		Product = p;
		ProductId = p.Id;
		Quantity = qtd;
	}

	public SaleItem()
	{
		
	}
}
