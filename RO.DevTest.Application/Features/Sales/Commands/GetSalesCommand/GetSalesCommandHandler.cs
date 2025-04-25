using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;

namespace RO.DevTest.Application.Features.Sales.Commands.GetSalesCommand;
public class GetSalesCommandHandler(ISaleRepository saleRepository) : IRequestHandler<GetSalesCommandRequest, GetSalesResult>
{
	public async Task<GetSalesResult> Handle(GetSalesCommandRequest request, CancellationToken ct)
	{
		int page = request.Page;
		int size = request.Size == 0 ? 10 : request.Size;

		var sales = await saleRepository.GetPagedSalesAsync(page, size, ct);

		return new GetSalesResult()
		{
			Page = page,
			Size = sales.Count,
			Sales = sales
		};
	}
}
