using BE.Application.Contracts.Persistance.Repositories;
using MediatR;

namespace BE.Application.Features.Sales.Commands.DeleteSaleCommand;

public class DeleteSaleCommandHandler(ISaleRepository saleRepository) : IRequestHandler<DeleteSaleCommandRequest, DeleteSaleCommandResponse?>
{
    public async Task<DeleteSaleCommandResponse?> Handle(DeleteSaleCommandRequest request, CancellationToken ct)
    {
        var sale = await saleRepository.GetSaleById(request.Id, ct);

        if (sale is null)
            return new DeleteSaleCommandResponse(){ Success = true };
        
        saleRepository.Delete(sale);
        await saleRepository.SaveChangesAsync(ct);
        return new DeleteSaleCommandResponse() { Success = true };
    }
}