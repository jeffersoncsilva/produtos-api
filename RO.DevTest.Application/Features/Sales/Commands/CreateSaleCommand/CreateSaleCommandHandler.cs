using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Sales.Commands.CreateSaleCommand;

public class CreateSaleCommandHandler(IProductsRepository productRepository, ISaleRepository saleRepository) : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken ct)
    {
        var validator = new CreateSaleValidator();
        var resultValidation = await validator.ValidateAsync(request, ct);

        if (!resultValidation.IsValid)
            throw new BadRequestException(resultValidation);

		var sale = CreateSale(request);
		sale.Itens = await GetSaleItens(request.Products, sale, ct);

        var result = await saleRepository.CreateAsync(sale, ct);

        return new CreateSaleResult() { SaleId = sale.Id };
	}

    private async Task<ICollection<SaleItem>> GetSaleItens(IEnumerable<ProductSaleCommand> products, Sale sale, CancellationToken ct)
    {
        ICollection<SaleItem> saleItems = [];

		foreach (var ps in products)
		{
			var product = await productRepository.GetProductAsync(p => p.Id == ps.ProductId, ct);
			if (product is null)
				throw new BadRequestException($"Produto de Id {ps.ProductId} não foi encontrado.");
			saleItems.Add(new SaleItem(product, ps.Quantity, sale));
		}

		return saleItems;
	}

	private Sale CreateSale(CreateSaleCommand request)
	{
		return new Sale()
		{
			Observation = request.Observation,
			Price = request.Price
		};
	}
}