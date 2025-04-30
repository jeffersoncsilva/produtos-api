using BE.Application.Contracts.Persistance.Repositories;
using MediatR;
using BE.Domain.Entities;
using BE.Domain.Exception;

namespace BE.Application.Features.Sales.Commands.CreateSaleCommand;

public class CreateSaleCommandHandler(IProductsRepository productRepository, ISaleRepository saleRepository) : IRequestHandler<CreateSaleRequest, CreateSaleResult>
{
    public async Task<CreateSaleResult> Handle(CreateSaleRequest request, CancellationToken ct)
    {
	    try
	    {
		    var validator = new CreateSaleValidator();
		    var resultValidation = await validator.ValidateAsync(request, ct);

		    if (!resultValidation.IsValid)
			    throw new BadRequestException(resultValidation);

		    var sale = CreateSale(request);
		    sale.Itens = await GetSaleItens(request.Products, sale, ct);

		    if (sale.Itens.Count == 0)
			    throw new EstoqueProdutoInsuficienteException(Guid.Empty, "Nenhum produto foi adicionado a venda.");
		    
		    var result = await saleRepository.CreateAsync(sale, ct);

		    return new CreateSaleResult
		    {
			    SaleId = result.Id,
			    Success = true,
			    Errors = null
		    };
	    }
	    catch (EstoqueProdutoInsuficienteException ex)
	    {
		    return new CreateSaleResult()
		    {
			    Errors = [ ex.Message ],
			    Success = false,
			    SaleId = Guid.Empty
		    };
	    }
	    catch (Exception ex)
	    {
		    return new CreateSaleResult()
		    {
			    Errors = [ ex.Message ],
			    Success = false,
			    SaleId = Guid.Empty
		    };
	    }
	}

    private async Task<ICollection<SaleItem>> GetSaleItens(IEnumerable<ProductSaleCommand> products, Sale sale, CancellationToken ct)
    {
        ICollection<SaleItem> saleItems = [];

		foreach (var ps in products)
		{
			var product = await productRepository.GetProductAsync(p => p.Id == ps.ProductId && p.Stock >= ps.Quantity && p.IsActive, ct);
			if (product is null)
				throw new EstoqueProdutoInsuficienteException(ps.ProductId, "Verifique se há estoque disponível do produto e se ele está ativo.");
			product.Stock -= ps.Quantity;
			productRepository.Update(product);
			saleItems.Add(new SaleItem(product, ps.Quantity, sale));
		}

		return saleItems;
	}

	private Sale CreateSale(CreateSaleRequest request)
	{
		return new Sale()
		{
			Observation = request.Observation,
			Price = request.Price,
			Name = request.Name ?? string.Empty,
			Descount = request.Descount
		};
	}
}