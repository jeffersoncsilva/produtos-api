using MediatR;

namespace RO.DevTest.FronEnd.Application.Features.Products.GetProductsCommand;

public class GetProductCommandRequest(int page, int size) : IRequest<GetProductCommandResponse>
{
	public int Page { get; init; } = page;
	public int Size { get; init; } = size;
}
