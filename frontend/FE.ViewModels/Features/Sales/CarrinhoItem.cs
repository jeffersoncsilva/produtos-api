using FE.ViewModels.Features.Product;

namespace FE.ViewModels.Features.Sales;

public class CarrinhoItem(ProductSimpleViewModel? produto, int quantidade)
{
	public ProductSimpleViewModel? Produto { get; init; } = produto ?? throw new ArgumentException("Produto nulo adicionado no carrinho.");
	public int Quantidade { get; set; } = quantidade;

	public decimal ValorTotalItem => (Produto?.Price ?? 0M) * Quantidade;
}