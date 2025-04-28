using MediatR;

namespace FE.Application.Features.Products.GetProductByIdCommand;

public sealed class GetProductByIdCommandRequest(Guid? id) : IRequest<GetProductByIdResponse>
{
	public Guid Id { get; init; } = id ?? Guid.Empty;
}
