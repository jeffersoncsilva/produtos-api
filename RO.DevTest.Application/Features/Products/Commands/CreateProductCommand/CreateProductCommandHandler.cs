using FluentValidation.Results;
using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Products.Commands.CreateProductCommand;

public class CreateProductCommandHandler(IProductsRepository productsRepository) : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken ct)
    {
        CreateProductValidator validator = new();
        ValidationResult validationResult = await validator.ValidateAsync(request, ct);

        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);

        var product = request.ToEntity();

        var result = await productsRepository.CreateAsync(product, ct);
        
        if (result is null)
            throw new BadRequestException("Erro ao criar o produto.");

        return new CreateProductResult()
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description,
            Price = result.Price,
            CreatedBy = result.CreatedBy
        };
    }
}