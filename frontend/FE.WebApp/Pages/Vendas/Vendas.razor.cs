using FE.Application.Features.Sales.DeleteSaleCommand;
using FE.Application.Features.Sales.GetSalesCommand;
using FE.ViewModels;
using FE.WebApp.Services;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace FE.WebApp.Pages.Vendas;

public partial class Vendas
{
	[Inject] public IMediator Mediator { get; set; } = default!;
	[Inject] public NavigationManager NavManager { get; set; } = default!;
	[Inject] public ConfirmationService ConfirmationService { get; set; } = default!;


	private GetSalesResponse? _vendasRealizadas;
	private int _paginaAtual = 12;
	private int _tamanhoPagina = 10;
	private int _totalDeItens;

	private bool _carregando;
	private bool _removendoItem;
	private bool _erroAoCarregarItens;
	private bool _erroAoRemoverItem;

	protected override async Task OnInitializedAsync()
	{
		await CarregarVendas();
		await base.OnInitializedAsync();
	}

	private async Task CarregarVendas()
	{
		_carregando = true;
		_erroAoCarregarItens = false;
		StateHasChanged();

		var resultado = await Mediator.Send(new GetSalesRequest(_paginaAtual, _tamanhoPagina));
		if (resultado is { Status: EStatusResponse.Ok, Dado: not null })
		{
			_vendasRealizadas = resultado.Dado;
			_paginaAtual = resultado.Dado.Page;
			_tamanhoPagina = resultado.Dado.Size;
			_totalDeItens = resultado.Dado.TotalSales;
			Console.WriteLine("Total de itens: " + _totalDeItens);
		}
			
		else
			_erroAoCarregarItens = true;

		
		_carregando = false;
		StateHasChanged();
	}

	private void EditarVenda(SaleItem? item)
	{
		if (item is null)
			return;

		NavManager.NavigateTo($"editar-venda?id={item.SaleId}");
	}

	private async Task RemoverVenda(SaleItem? item)
	{
		var confirmacao = await ConfirmationService.Show("Deletar Venda", "Deseja realmente apagar essa venda?", "Deletar", "Cancelar");
		if (confirmacao)
		{
			_removendoItem = true;
			var resultado = await Mediator.Send(new DeleteSaleRequest(item.SaleId));
			if (resultado is { Status: EStatusResponse.Ok, Dado.Success: true })
				await CarregarVendas();
			else
				_erroAoRemoverItem = true;

			_removendoItem = false;
			StateHasChanged();
		}
	}

	private void InformacoesVenda(SaleItem? item)
	{
		if (item is not null) 
			NavManager.NavigateTo($"vendas-detalhes?id={item.SaleId}");
	}

	private async Task CarregarProximaPaginaVendas()
	{
		_paginaAtual++;
		await CarregarVendas();
	}

	private async Task CarregarPaginaAnteriorVendas()
	{
		_paginaAtual--;
		await CarregarVendas();
	}
}