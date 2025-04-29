using FE.ViewModels;
using MediatR;

namespace FE.Application.Features.Products.GetProductsCommand;

public class GetProductRequest(int page, int size) : IRequest<BaseResponse<GetProductResponse?>>
{
	public int Page { get; init; } = page;
	public int Size { get; init; } = size;
}
