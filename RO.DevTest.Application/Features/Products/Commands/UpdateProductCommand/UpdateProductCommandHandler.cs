using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Products.Commands.UpdateProductCommand;

public class UpdateProductCommandHandler(IProductsRepository productRepository) : IRequestHandler<UpdateProductCommandRequest, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommandRequest request, CancellationToken ct)
    {
        var validator = new UpdateProductValidator();
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);

        var entitie = await productRepository.GetProductAsync(p => p.Id == request.Id, ct);
        
        if(entitie is null)
            throw new BadRequestException("Nenhum produto encontrado com o ID informado.");

        var entitieToUpdate = request.ToEntity();
        
        productRepository.Delete(entitie);
        productRepository.Update(entitieToUpdate);
        
        await productRepository.SaveChangesAsync(ct);

        return new UpdateProductResult()
        {
            Id = entitieToUpdate.Id
        };
    }
}