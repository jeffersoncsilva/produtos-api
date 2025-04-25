using MediatR;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Features.Sales.Commands.CreateSaleCommand;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
	public string Observation { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public decimal Descount { get; set; }
	public IEnumerable<ProductSaleCommand> Products { get; set; } = [];
}

public sealed class ProductSaleCommand
{
	public Guid ProductId { get; set; }
	public int Quantity { get; set; }
}

