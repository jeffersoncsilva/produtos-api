using FE.Application.Features.Products.GetProductsCommand;
using FE.ViewModels;
using FE.ViewModels.Features.Product;
using FE.ViewModels.Features.Sales;
using FE.WebApp.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace FE.WebApp.Pages.Produtos;

public partial class ListarProdutos
{
	[Inject] public IMediator Mediator { get; set; } = default!;
	[Inject] public NavigationManager NavManager { get; set; } = default!;
	[Inject] public ICarrinhoCompraServico CarrinhoDeCompra { get; set; } = default!;

	private int _page = 0;
	private int _size = 12;
	private int _totalProdutos;
	private IEnumerable<ProductSimpleViewModel>? _products;
	private readonly List<ProductSimpleViewModel> _totalProductsLoaded = new();
	private bool _exibeMensagemErro = false;
	private bool _carregando;

	protected override async Task OnInitializedAsync()
	{
		await CarregarProdutos();
		await base.OnInitializedAsync();
	}

	private async Task CarregarProdutos()
	{
		_exibeMensagemErro = false;
		_carregando = true;

		_products = _totalProductsLoaded.Skip(_page * _size).Take(_size).ToList();

		if (_products.Count() == _size)
		{
			_carregando = false;
			return;
		}

		var resultado = await Mediator.Send(new GetProductRequest(_page, _size));
		if (resultado is { Status: EStatusResponse.Ok, Dado: not null })
		{
			var dados = resultado.Dado;
			_totalProductsLoaded.AddRange(dados.Products!);
			_products = dados.Products;
			_page = dados.Page;
			_size = dados.Size;
			_totalProdutos = dados.TotalProducts;
		}
		else
		{
			_exibeMensagemErro = true;
		}
		_carregando = false;
	}

	private async Task CarregarProximaPaginaProdutos()
	{
		_page++;
		await CarregarProdutos();
	}

	private async Task CarregarPaginaAnteriorProdutos()
	{
		_page--;
		await CarregarProdutos();
	}

	private void NavegaPaginaNovoProduto()
	{
		NavManager.NavigateTo("novo-produto");
	}

	private void EditarProduto(ProductSimpleViewModel? produto)
	{
		if (produto is not null)
			NavManager.NavigateTo($"editar-produto?id={produto.Id}");
	}

	private void AdicionarCarrinhoCompra(ProductSimpleViewModel? produto)
	{
		if (produto is not null)
		{
			var item = new CarrinhoItem(produto, 1);
			CarrinhoDeCompra.AdicionaItemNoCarrinho(item);
		}
	}

	private void MostraInformacoesProduto(ProductSimpleViewModel? produto)
	{
		if (produto is not null)
			NavManager.NavigateTo($"detalhes-produto?id={produto.Id}");
	}
}