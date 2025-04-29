using System.Security.Claims;
using FE.Application.Features.Sales.CreateSaleCommand;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using FE.ViewModels;
using FE.ViewModels.Features.Sales;
using FE.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace FE.WebApp.Pages.CarrinhoDeCompra;

public partial class CarrinhoCompra : IDisposable
{

	[Inject] public IMediator Mediator { get; set; } = default!;
	[Inject] public NavigationManager NavManager { get; set; } = default!;
	[Inject] public ICarrinhoCompraServico CarrinhoDeCompra { get; set; } = default!;
	[Inject] public AuthenticationStateProvider AuthApi { get; set; } = default!;


	private EditContext? _editContext;
	private ValidationMessageStore? _messageStore;
	private CreateSaleRequest? _request;
	

	private bool _cadastrandoVenda;
	private bool _erroCadastarVenda;
	private string _messageErroCadastrarVenda = string.Empty;

	protected override void OnInitialized()
	{
		_request = new CreateSaleRequest();
		_editContext = new EditContext(_request);
		_editContext.OnValidationRequested += HandleValidation;
		_messageStore = new ValidationMessageStore(_editContext);
		base.OnInitialized();
	}

	public async Task CadastrarVenda()
	{
		if (_request is null)
			return;
		_cadastrandoVenda = true;
		StateHasChanged();
		_request.Itens = CarrinhoDeCompra.Produtos.Select(i => new ProductsSale(i.Produto.Id, i.Quantidade)).ToList();
		_request.Price = CarrinhoDeCompra.ValorTotalCarrinho();
		var user = await ((ApiAuthenticationStateProvider)AuthApi).GetAuthenticationStateAsync();
		_request.CreatedBy = user?.User?.FindFirst(f => f.Type == ClaimTypes.Email)?.Value ?? string.Empty;
		var resultado = await Mediator.Send(_request);
		if (resultado is { Status: EStatusResponse.Ok, Dado.Success: true } && resultado.Dado.SaleId != Guid.Empty)
			NavManager.NavigateTo($"vendas-detalhes?id={resultado.Dado.SaleId}");
		else
		{
			_erroCadastarVenda = true;
			_messageErroCadastrarVenda = string.Join(", ", resultado.Dado?.Errors ?? []);
			if (string.IsNullOrWhiteSpace(_messageErroCadastrarVenda))
				_messageErroCadastrarVenda = "Ocorreu um erro ao cadastrar a venda. Tente novamente.";
		}

		_cadastrandoVenda = false;
		StateHasChanged();
	}

	public void Dispose()
	{
		if (_editContext is not null)
			_editContext.OnValidationRequested -= HandleValidation;
	}

	private void HandleValidation(object? sender, ValidationRequestedEventArgs args)
	{
		_messageStore?.Clear();

		if (_request is null)
			return;

		if (_request!.Descount < 0.0M)
			_messageStore?.Add(() => _request!.Descount, "Valor de desconto não pode ser menor que zero.");

		if (string.IsNullOrWhiteSpace(_request!.Observation))
			_messageStore?.Add(() => _request.Observation, "Observação não pode ser vazia.");

		if (_request!.Observation.Length > 2048)
			_messageStore?.Add(() => _request.Observation, "Observação não pode ter mais de 2048 caracteres.");

		if (string.IsNullOrWhiteSpace(_request!.Name))
			_messageStore?.Add(() => _request!.Name, "Nome da venda não pode ser vazio.");

		if (_request!.Name.Length > 128)
			_messageStore?.Add(() => _request!.Name, "Nome não pode ter mais de 128 caracteres.");
	}

	private void AdicionarUnidadeItem(CarrinhoItem item) => CarrinhoDeCompra.AdicionaItemNoCarrinho(item);

	private void RemoverUnidadeItem(CarrinhoItem item) => CarrinhoDeCompra.RemoverItemDoCarrinho(item);
	
}