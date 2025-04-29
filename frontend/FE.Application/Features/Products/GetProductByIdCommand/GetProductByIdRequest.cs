using FE.ViewModels;
using MediatR;

namespace FE.Application.Features.Products.GetProductByIdCommand;

public sealed class GetProductByIdCommandRequest(Guid? id) : IRequest<BaseResponse<GetProductByIdResponse?>>
{
	public Guid Id { get; init; } = id ?? Guid.Empty;
}
