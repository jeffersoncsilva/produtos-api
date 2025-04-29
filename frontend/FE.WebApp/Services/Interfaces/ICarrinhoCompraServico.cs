using FE.ViewModels.Features.Sales;

namespace FE.WebApp.Services.Interfaces;

public interface ICarrinhoCompraServico
{
	IList<CarrinhoItem> Produtos { get; set; }
	event Action? OnCarrinhoChanged;

	void AdicionaItemNoCarrinho(CarrinhoItem produto);

	void RemoverItemDoCarrinho(CarrinhoItem produto);

	void LimparCarrinho();

	int QuantidadeProdutos();

	int QuantidadeItens();

	ValueTask ValidarDisponibilidadeItens();

	decimal ValorTotalCarrinho();
}