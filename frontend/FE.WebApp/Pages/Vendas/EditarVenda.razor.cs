using FE.Application.Features.Sales.GetSaleByIdCommand;
using FE.Application.Features.Sales.UpdateSaleCommand;
using FE.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FE.WebApp.Pages.Vendas;

public partial class EditarVenda : IDisposable
{
	[Inject] public IMediator Mediator { get; set; } = default!;
	[Inject] public NavigationManager NavManager { get; set; } = default!;

	[Parameter]
	[SupplyParameterFromQuery(Name = "id")]
	public Guid Id { get; set; }

	private EditContext? _editContext;
	private ValidationMessageStore? _messageStore;
	private UpdateSaleRequest? _request;
	private int Quantidade => _request?.Itens.Count ?? 0;

	private bool _carregando;
	private bool _erroAoCarregarDados;
	private bool _atualizandoVenda;
	private bool _erroAoAtualizar;

	protected override async Task OnInitializedAsync()
	{
		await CarregaDadosVendaParaEdicao();
		await base.OnInitializedAsync();
	}

	private async ValueTask CarregaDadosVendaParaEdicao()
	{
		_carregando = true;
		_erroAoCarregarDados = false;
		_atualizandoVenda = false;
		_erroAoAtualizar = false;

		var resultado = await Mediator.Send(new GetSaleDetailByIdRequest(Id));
		if (resultado.Status != EStatusResponse.Ok || resultado.Dado is null)
		{
			_erroAoCarregarDados = true;
			_carregando = false;
			return;
		}

		var venda = resultado.Dado;
		_request = new UpdateSaleRequest
		{
			Id = venda.Id,
			Observation = venda.Observatin,
			Descount = venda.Descount,
			Price = venda.Price,
			Itens = venda.Itens.Select(i => new SaleItemUpdate() { Id = i.ProductId, Quantity = i.Quantity, ProductName = i.ProductName, ProductPrice = i.ProductPrice }).ToList()
		};
		_editContext = new EditContext(_request);
		_editContext.OnValidationRequested += HandleValidation;
		_messageStore = new ValidationMessageStore(_editContext);

		_carregando = false;
	}

	public async Task EnviarVenda()
	{
		if (_request is null)
			return;

		_atualizandoVenda = true;
		_carregando = false;
		_erroAoCarregarDados = false;
		_erroAoAtualizar = false;

		var resultado = await Mediator.Send(_request);

		if (resultado is { Status: EStatusResponse.Ok, Dado: not null } && resultado.Dado.SaleIdUpdated != Guid.Empty)
			NavManager.NavigateTo($"vendas-detalhes?id={resultado.Dado.SaleIdUpdated}");
		else
		{
			_erroAoAtualizar = true;
		}

		_atualizandoVenda = false;
	}

	private void HandleValidation(object? sender, ValidationRequestedEventArgs args)
	{
		 _messageStore?.Clear();

		 if (_request?.Price <= 0.0M)
		     _messageStore?.Add(() => _request.Price, "Preço não pode ser menor que zero.");

		 if (_request?.Descount < 0.0M)
		     _messageStore?.Add(() => _request.Descount, "Valor de desconto não pode ser menor que zero.");

		 if(RequestNaoTemItens())
			 _messageStore?.Add(() => _request!.Itens, "Não pode atualizar uma venda sem produtos!");
	}

	private void RemoverItem(SaleItemUpdate itemUpdate)
	{
		if (RequestNaoTemItens())
			return;

		int idx = _request!.Itens.IndexOf(itemUpdate);
		if (idx >= 0)
		{
			if (_request!.Itens[idx].Quantity == 1)
				_request!.Itens.RemoveAt(idx);
			else
				_request!.Itens[idx].Quantity -= 1;
		}

		RecalcularValorDaVenda();
	}

	private void AdicionarItem(SaleItemUpdate itemUpdate)
	{
		if (RequestNaoTemItens())
			return;

		int idx = _request!.Itens.IndexOf(itemUpdate);
		if (idx >= 0)
		{
			_request!.Itens[idx].Quantity += 1;
		}

		RecalcularValorDaVenda();
	}

	public void Dispose()
	{
		if (_editContext is not null)
			_editContext.OnValidationRequested -= HandleValidation;
	}

	private void RecalcularValorDaVenda()
	{
		if (RequestNaoTemItens())
			return;

		_request!.Price = _request.Itens.Sum(i => i.Quantity * i.ProductPrice);
	}

	private bool RequestNaoTemItens() => _request is { Itens.Count: 0 };
}