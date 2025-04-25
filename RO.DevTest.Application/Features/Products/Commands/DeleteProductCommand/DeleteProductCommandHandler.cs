using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Products.Commands.DeleteProductCommand;

public class DeleteProductCommandHandler(IProductsRepository repository) : IRequestHandler<DeleteProductCommandRequest, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommandRequest request, CancellationToken ct)
    {
        var product = await repository.GetProductAsync(p => p.Id == request.Id, ct);
        if (product is null)
            throw new BadRequestException("Id do produto não encontrado.");

        repository.Delete(product);

        return new DeleteProductResult();
    }
}