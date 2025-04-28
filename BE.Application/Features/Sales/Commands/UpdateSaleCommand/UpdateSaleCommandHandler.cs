using BE.Application.Contracts.Persistance.Repositories;
using MediatR;
using RO.DevTest.Domain.Exception;

namespace BE.Application.Features.Sales.Commands.UpdateSaleCommand;

public sealed class UpdateSaleCommandHandler(ISaleRepository saleRepository): IRequestHandler<UpdateSaleCommandRequest, UpdateSaleCommandReponse>
{
    public async Task<UpdateSaleCommandReponse> Handle(UpdateSaleCommandRequest request, CancellationToken ct)
    {
        var sale = await saleRepository.GetSaleById(request.Id, ct);
        if (sale is null)
            throw new BadRequestException("Venda não encontrada");

        sale.Descount = request.Descount;
        sale.Observation = request.Observatin ?? "";
        sale.Price = request.Price;

        foreach (var item in request.Itens)
        {
            var product = sale.Itens!.FirstOrDefault(i => i.ProductId == item.Id);
            if (product is null)
                continue;

            product.Quantity = item.Quantity;
        }

        saleRepository.Update(sale);
        await saleRepository.SaveChangesAsync(ct);
        return new UpdateSaleCommandReponse() { SaleIdUpdated = sale.Id };
    }
}