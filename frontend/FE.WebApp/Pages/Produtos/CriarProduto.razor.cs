using System.Security.Claims;
using FE.Application.Features.Products.CreateProductCommand;
using FE.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace FE.WebApp.Pages.Produtos;

public partial class CriarProduto : IDisposable
{
	[Inject] public IMediator Mediator { get; set; } = default!;
	[Inject] public NavigationManager NavManager { get; set; } = default!;
	[Inject] public AuthenticationStateProvider AuthApi { get; set; } = default!;

	private CreateProductRequest? _request;
	private EditContext? _editContext;
	private ValidationMessageStore? _messageStore;

	private bool _salvandoProduto;
	private bool _erroAoSalvarProduto;


	protected override void OnInitialized()
	{
		_request = new CreateProductRequest();
		_editContext = new EditContext(_request);
		_editContext.OnValidationRequested += HandleValidationRequested;
		_messageStore = new ValidationMessageStore(_editContext);
		base.OnInitialized();
	}

	private async Task EnviarDadosFormularioValido()
	{
		_salvandoProduto = true;
		_erroAoSalvarProduto = false;
		StateHasChanged();
		var user = await ((ApiAuthenticationStateProvider)AuthApi).GetAuthenticationStateAsync();
		if (_request != null)
		{
			_request.CreatedBy = user?.User?.FindFirst(f => f.Type == ClaimTypes.Email)?.Value ?? string.Empty;
			var resultado = await Mediator.Send(_request!);
			if (resultado is { Status: EStatusResponse.Ok, Dado: not null } && resultado.Dado.Id != Guid.Empty)
			{
				NavManager.NavigateTo($"/detalhes-produto?id={resultado.Dado.Id}");
			}
			else
			{
				_erroAoSalvarProduto = true;
			}
		}

		_salvandoProduto = false;
		StateHasChanged();
	}

	private void HandleValidationRequested(object? sender, ValidationRequestedEventArgs args)
	{
		_messageStore?.Clear();
		if (string.IsNullOrWhiteSpace(_request?.Name))
			_messageStore?.Add(() => _request!.Name!, "Nome é Obrigatório.");
		if (string.IsNullOrWhiteSpace(_request?.Description))
			_messageStore?.Add(() => _request!.Description!, "Descrição é Obrigatório.");
		if (_request?.Price <= 0)
			_messageStore?.Add(() => _request.Price, "Valor maior que zero é Obrigatório.");
		if (string.IsNullOrWhiteSpace(_request?.ImageUrl))
			_messageStore?.Add(() => _request!.ImageUrl!, "A URL Da imagem e Obrigatório.");
		if (string.IsNullOrWhiteSpace(_request?.Category))
			_messageStore?.Add(() => _request!.Category!, "A categoria e Obrigatório.");
		if (string.IsNullOrWhiteSpace(_request?.Brand))
			_messageStore?.Add(() => _request!.Brand!, "A Marca e Obrigatório.");
		if (_request!.Stock <= 0)
			_messageStore?.Add(() => _request.Stock, "A quantidade deve ser maior ou igual a 1.");
		if (!_request!.IsActive)
			_messageStore?.Add(() => _request.IsActive, "Não pode adicionar um produto inativo.");
	}

	public void Dispose()
	{
		if (_editContext is not null)
		{
			_editContext.OnValidationRequested -= HandleValidationRequested;
		}
	}
}