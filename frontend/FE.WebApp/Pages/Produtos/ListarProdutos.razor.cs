using FE.Application.Features.Products.GetProductsCommand;
using FE.ViewModels;
using FE.ViewModels.Features.Product;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace FE.WebApp.Pages.Produtos;

public partial class ListarProdutos
{
	[Inject] public IMediator Mediator { get; set; } = default!;
	[Inject] public NavigationManager NavManager { get; set; } = default!;


	private int _page = 0;
	private int _size = 5;
	private IEnumerable<ProductSimpleViewModel>? _products;
	private bool _exibeMensagemErro = false;
	private bool _carregando;

	protected override async Task OnInitializedAsync()
	{
		await CarregarProdutos();
		await base.OnInitializedAsync();
	}

	private async ValueTask CarregarProdutos()
	{
		_exibeMensagemErro = false;
		_carregando = true;
		var resultado = await Mediator.Send(new GetProductRequest(_page, _size));
		if (resultado is { Status: EStatusResponse.Ok, Dado: not null })
		{
			var dados = resultado.Dado;
			_products = dados.Products;
			_page = dados.Page;
			_size = dados.Size;
		}
		else
		{
			_exibeMensagemErro = true;
		}
		_carregando = false;
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

	private void VenderProduto(ProductSimpleViewModel? produto)
	{
		if (produto is not null)
			NavManager.NavigateTo($"vender-produto?id={produto.Id}&nome-produto={produto.Name}&stock={produto.Stock}");
	}
}