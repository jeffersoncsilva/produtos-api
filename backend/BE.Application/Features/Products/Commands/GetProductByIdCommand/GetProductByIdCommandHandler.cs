using BE.Application.Contracts.Persistance.Repositories;
using MediatR;
using BE.Domain.Exception;

namespace BE.Application.Features.Products.Commands.GetProductByIdCommand;

public class GetProductByIdCommandHandler(IProductsRepository repository) : IRequestHandler<GetProductByIdCommand, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdCommand request, CancellationToken ct)
    {
        var product = await repository.GetProductAsync(p => p.Id == request.Id, ct);
        if (product is null)
            throw new BadRequestException("Id informado não foi encontrado.");

        return new GetProductByIdResult()
        {
            Id = product.Id,
            CreatedOn = product.CreatedOn,
            ModifiedOn = product.ModifiedOn,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Category = product.Category,
            Brand = product.Brand,
            Stock = product.Stock,
            IsActive = product.IsActive,
            CreatedBy = product.CreatedBy,
            ModifiedBy = product.ModifiedBy
        };
    }
}