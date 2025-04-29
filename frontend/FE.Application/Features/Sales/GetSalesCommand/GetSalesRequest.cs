using FE.ViewModels;
using MediatR;

namespace FE.Application.Features.Sales.GetSalesCommand;

public class GetSalesRequest(int page, int size) : IRequest<BaseResponse<GetSalesResponse?>>
{
	public int Page { get; init; } = page;
	public int Size { get; init; } = size;
}

