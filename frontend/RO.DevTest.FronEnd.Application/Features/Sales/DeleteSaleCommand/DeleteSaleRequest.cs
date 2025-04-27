using MediatR;

namespace RO.DevTest.FronEnd.Application.Features.Sales.DeleteSaleCommand;

public class DeleteSaleRequest(Guid id) : IRequest<DeleteSaleResponse>
{
	public Guid Id { get; init; } = id;
}
