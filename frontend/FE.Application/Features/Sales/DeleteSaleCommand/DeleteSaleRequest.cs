using FE.ViewModels;
using MediatR;

namespace FE.Application.Features.Sales.DeleteSaleCommand;

public class DeleteSaleRequest(Guid id) : IRequest<BaseResponse<DeleteSaleResponse?>>
{
	public Guid Id { get; init; } = id;
}
