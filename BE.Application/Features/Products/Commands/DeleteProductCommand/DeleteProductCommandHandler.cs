using BE.Application.Contracts.Persistance.Repositories;
using MediatR;
using BE.Domain.Exception;

namespace BE.Application.Features.Products.Commands.DeleteProductCommand;

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