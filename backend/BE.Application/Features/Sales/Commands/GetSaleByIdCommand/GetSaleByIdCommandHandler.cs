using BE.Application.Contracts.Persistance.Repositories;
using MediatR;

namespace BE.Application.Features.Sales.Commands.GetSaleByIdCommand;

public class GetSaleByIdCommandHandler(ISaleRepository saleRepository) : IRequestHandler<GetSaleByIdCommandRequest, GetSaleByIdCommandResponse?>
{
    public async Task<GetSaleByIdCommandResponse?> Handle(GetSaleByIdCommandRequest request, CancellationToken ct)
    {
        try
        {
            var sale = await saleRepository.GetSaleById(request.Id, ct);
            if (sale is null)
                return null;

            return new GetSaleByIdCommandResponse()
            {
                Id = sale.Id,
                Observatin = sale.Observation,
                Price = sale.Price,
                Descount = sale.Descount,
                Itens = sale.Itens?.Select(i => new SaleItemResponse()
                {
                    ProductId = i.ProductId,
                    ProductName = i?.Product?.Name ?? string.Empty,
                    ProductDescription = i?.Product?.Description ?? string.Empty,
                    ProductCategory = i?.Product?.Category ?? string.Empty,
                    ProductBrand = i?.Product?.Brand ?? string.Empty,
                    ProductImageUrl = i?.Product?.ImageUrl ?? string.Empty,
                    Quantity = i?.Quantity ?? 0,
                    ProductPrice = i?.Product?.Price ?? 0m
                }).ToList() ?? []
            };
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}