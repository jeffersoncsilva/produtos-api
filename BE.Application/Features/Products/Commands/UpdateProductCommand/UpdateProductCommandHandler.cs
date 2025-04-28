using BE.Application.Contracts.Persistance.Repositories;
using MediatR;
using BE.Domain.Exception;

namespace BE.Application.Features.Products.Commands.UpdateProductCommand;

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

        entitie.Name = request.Name;
        entitie.Description = request.Description;
        entitie.Price = request.Price;
        entitie.ImageUrl = request.ImageUrl;
        entitie.Category = request.Category;
        entitie.Brand = request.Brand;
        entitie.Stock = request.Stock;
        entitie.IsActive = request.IsActive;
        entitie.ModifiedBy = request.ModifiedBy;

        productRepository.Update(entitie);
        await productRepository.SaveChangesAsync(ct);

        return new UpdateProductResult(entitie.Id);
    }
}