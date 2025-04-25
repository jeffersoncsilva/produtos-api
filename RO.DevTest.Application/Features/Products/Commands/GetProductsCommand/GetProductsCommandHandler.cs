using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;

namespace RO.DevTest.Application.Features.Products.Commands.GetProductsCommand;

public class GetProductsCommandHandler(IProductsRepository productsRepository) : IRequestHandler<GetProductsRequest, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsRequest request, CancellationToken ct)
    {
        var products = await productsRepository.GetPagedProducts(request.Page, request.PageSize, ct);

        return new GetProductsResult
        {
            Products = products.Select(p => new ProductsSimpleResult() {  Id = p.Id, Name = p.Name, Price = p.Price }).ToList(),
            Page = request.Page,
            Size = products.Count
        };
    }
}