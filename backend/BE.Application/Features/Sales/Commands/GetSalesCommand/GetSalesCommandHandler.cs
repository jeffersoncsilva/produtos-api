using BE.Application.Contracts.Persistance.Repositories;
using MediatR;

namespace BE.Application.Features.Sales.Commands.GetSalesCommand;
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
			Sales = sales.Select(s => new SaleItem()
			{
				SaleId = s.Id, 
				Name = s.Name,
				Descount = s.Descount, 
				Observation = s.Observation, 
				Price = s.Price, 
				ProductsName = s.Itens!.Select(i => i.Product!.Name).ToList()!
			}).ToList()
		};
	}
}
