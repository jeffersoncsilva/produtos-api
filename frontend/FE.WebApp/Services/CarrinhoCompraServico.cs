using FE.ViewModels.Features.Sales;
using FE.WebApp.Services.Interfaces;

namespace FE.WebApp.Services;

public sealed class CarrinhoCompraServico : ICarrinhoCompraServico
{
	public IList<CarrinhoItem> Produtos { get; set; } = new List<CarrinhoItem>();

	public event Action? OnCarrinhoChanged;

	public void AdicionaItemNoCarrinho(CarrinhoItem novoItem)
	{
		var item = Produtos.FirstOrDefault(i => i.Produto?.Id == novoItem.Produto?.Id);
		if (item is null)
		{
			Produtos.Add(novoItem!);
		}
		else
		{
			var idx = Produtos.IndexOf(item);
			if (idx >= 0)
				Produtos[idx].Quantidade += 1;
		}
		OnCarrinhoChanged?.Invoke();
	}

	public void RemoverItemDoCarrinho(CarrinhoItem itemParaRemover)
	{
		var item = Produtos.FirstOrDefault(i => i.Produto?.Id == itemParaRemover.Produto?.Id);
		if (item is not null)
		{
			var idx = Produtos.IndexOf(item);
			if (Produtos[idx].Quantidade == 1)
				Produtos.RemoveAt(idx);
			else
				Produtos[idx].Quantidade -= 1;
		}
		OnCarrinhoChanged?.Invoke();
	}

	public void LimparCarrinho()
	{
		Produtos.Clear();
		OnCarrinhoChanged?.Invoke();
	} 

	public decimal ValorTotalCarrinho() => Produtos.Sum(p => p.ValorTotalItem);

	public int QuantidadeItens() => Produtos.Sum(p => p.Quantidade);

	public int QuantidadeProdutos() => Produtos.Count();

	public ValueTask ValidarDisponibilidadeItens()
	{
		return ValueTask.CompletedTask;
	}
}