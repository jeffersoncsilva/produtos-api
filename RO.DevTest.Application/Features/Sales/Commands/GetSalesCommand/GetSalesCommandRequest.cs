using MediatR;

namespace RO.DevTest.Application.Features.Sales.Commands.GetSalesCommand;

public class GetSalesCommandRequest : IRequest<GetSalesResult>
{
	public int Page { get; init; }
	public int Size { get; init; }

	public GetSalesCommandRequest()
	{
		
	}

	public GetSalesCommandRequest(int page, int size)
	{
		Page = page;
		Size = size;
	}
}
