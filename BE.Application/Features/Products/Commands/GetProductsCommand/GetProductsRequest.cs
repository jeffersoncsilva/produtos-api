using MediatR;

namespace BE.Application.Features.Products.Commands.GetProductsCommand;

public class GetProductsRequest(int page, int pageSize) : IRequest<GetProductsResult>
{
    public int Page { get; init; } = page;
    public int PageSize { get; init; } = pageSize;
}