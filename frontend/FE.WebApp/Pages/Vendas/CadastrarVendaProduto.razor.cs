using FE.Application.Features.Sales.CreateSaleCommand;
using FE.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FE.WebApp.Pages.Vendas;

public partial class CadastrarVendaProduto : IDisposable
{
	[Inject] public IMediator Mediator { get; set; } = default!;
	[Inject] public NavigationManager NavManager { get; set; } = default!;

	[Parameter]
	[SupplyParameterFromQuery(Name = "id")]
	public Guid Id { get; set; }

	[Parameter]
	[SupplyParameterFromQuery(Name = "nome-produto")]
	public string NomeProduto { get; set; } = string.Empty;

	[Parameter]
	[SupplyParameterFromQuery(Name = "stock")]
	public int StockQuantity { get; set; }

	private EditContext? _editContext;
	private ValidationMessageStore? _messageStore;
	private CreateSaleRequest? _request;
	private int _quantidade;

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

	public async Task EnviarVenda()
	{
		if (_request is null)
			return;
		_cadastrandoVenda = true;
		StateHasChanged();

		_request.Itens.Add(new ProductsSale() { Id = Id, Quantity = _quantidade });
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
		if (_quantidade > StockQuantity)
			_messageStore?.Add(() => _quantidade, "Quantidade indisponível em estoque. Selecione um valor menor.");

		if (_request?.Price <= 0.0M)
			_messageStore?.Add(() => _request.Price, "Valor inválido calculado. Tente remover e adicionar um item novamente.");

		if (_request?.Descount < 0.0M)
			_messageStore?.Add(() => _request.Descount, "Valor de desconto não pode ser menor que zero.");

		if(string.IsNullOrWhiteSpace(_request.Observation))
			_messageStore?.Add(() => _request.Observation, "Observação não pode ser vazia.");

		if (_request?.Observation.Length > 2048)
			_messageStore?.Add(() => _request.Observation, "Observação não pode ter mais de 2048 caracteres.");

		if (string.IsNullOrWhiteSpace(_request?.Name))
			_messageStore?.Add(() => _request.Name, "Nome da venda não pode ser vazio.");

		if (_request?.Name?.Length > 128)
			_messageStore?.Add(() => _request.Name, "Nome não pode ter mais de 128 caracteres.");
	}
}