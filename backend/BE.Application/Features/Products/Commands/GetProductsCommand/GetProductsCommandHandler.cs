using BE.Application.Contracts.Persistance.Repositories;
using MediatR;

namespace BE.Application.Features.Products.Commands.GetProductsCommand;

public class GetProductsCommandHandler(IProductsRepository productsRepository) : IRequestHandler<GetProductsRequest, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsRequest request, CancellationToken ct)
    {
        var products = await productsRepository.GetPagedProducts(request.Page, request.PageSize, ct);

        return new GetProductsResult
        {
            Products = products.Select(p => new ProductsSimpleResult() 
                {  
                    Id = p.Id, 
                    Name = p.Name, 
                    Price = p.Price, 
                    Stock = p.Stock,
                    Description = p.Description
                }).ToList(),
            Page = request.Page,
            Size = products.Count
        };
    }
}