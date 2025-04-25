using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Features.Sales.Commands.GetSalesCommand;
public class GetSalesResult
{
	public IReadOnlyList<Sale> Sales { get; set; } = [];
	public int Page { get; set; }
	public int Size { get; set; }
}
