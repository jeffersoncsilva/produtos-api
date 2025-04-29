using System.Security.Claims;
using FE.Application.Features.Products.GetProductByIdCommand;
using FE.Application.Features.Products.UpdateProductCommand;
using FE.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace FE.WebApp.Pages.Produtos;

public partial class EditarProduto : IDisposable
{
	[Inject] public IMediator Mediator { get; set; } = default!;
	[Inject] public NavigationManager NavManager { get; set; } = default!;
	[Inject] public AuthenticationStateProvider AuthApi { get; set; } = default!;


	[Parameter]
	[SupplyParameterFromQuery(Name = "id")]
	public Guid? Id { get; set; }

	private UpdateProductRequest? _updateProduct;
	private EditContext? _editContext;
	private ValidationMessageStore? _messageStore;

	private bool _carregando = false;
	private bool _ocorreuErro = false;
	private string _mensagemErro = string.Empty;

	protected override async Task OnInitializedAsync()
	{
		await CarregaDadosProduto();
		await base.OnInitializedAsync();
	}

	private async ValueTask CarregaDadosProduto()
	{
		_ocorreuErro = false;
		_carregando = true;
		var resultado = await Mediator.Send(new GetProductByIdCommandRequest(Id));

		if (resultado is { Status: EStatusResponse.Ok, Dado: not null })
		{
			var productResult = resultado.Dado;
			_updateProduct = new UpdateProductRequest
			{
				Id = productResult.Id,
				Name = productResult.Name,
				Description = productResult.Description,
				Price = productResult.Price,
				ImageUrl = productResult.ImageUrl,
				Category = productResult.Category,
				Brand = productResult.Brand,
				Stock = productResult.Stock,
				IsActive = productResult.IsActive,
				CreatedBy = productResult.CreatedBy
			};
			// TODO: adicionar modifiedBy do usuário que está logado.
			_editContext = new EditContext(_updateProduct);
			_editContext.OnValidationRequested += HandleValidationRequested;
			_messageStore = new ValidationMessageStore(_editContext);
		}
		else
		{
			_ocorreuErro = true;
			_mensagemErro = "Não foi possivel recuperar o produto para edição.";
		}

		_carregando = false;
	}

	private void HandleValidationRequested(object? sender, ValidationRequestedEventArgs args)
	{
		if (_messageStore is null || _updateProduct is null)
			return;
		_messageStore.Clear();
		if (string.IsNullOrWhiteSpace(_updateProduct?.Name))
			_messageStore?.Add(() => _updateProduct!.Name!, "Nome é Obrigatório.");
		if (string.IsNullOrWhiteSpace(_updateProduct?.Description))
			_messageStore?.Add(() => _updateProduct!.Description!, "Descrição é Obrigatório.");
		if (_updateProduct?.Price <= 0)
			_messageStore?.Add(() => _updateProduct.Price, "Valor maior que zero é Obrigatório.");
		if (string.IsNullOrWhiteSpace(_updateProduct?.ImageUrl))
			_messageStore?.Add(() => _updateProduct!.ImageUrl!, "A URL Da imagem e Obrigatório.");
		if (string.IsNullOrWhiteSpace(_updateProduct?.Category))
			_messageStore?.Add(() => _updateProduct!.Category!, "A categoria e Obrigatório.");
		if (string.IsNullOrWhiteSpace(_updateProduct?.Brand))
			_messageStore?.Add(() => _updateProduct!.Brand!, "A Marca e Obrigatório.");
		if (_updateProduct!.Stock < 0)
			_messageStore?.Add(() => _updateProduct.Stock, "A quantidade deve ser maior ou igual a 1.");
	}

	private async Task AtualizarDadosFormulario()
	{
		_ocorreuErro = false;
		_carregando = true;
		var user = await ((ApiAuthenticationStateProvider)AuthApi).GetAuthenticationStateAsync();
		_updateProduct.ModifiedBy = user?.User?.FindFirst(f => f.Type == ClaimTypes.Email)?.Value ?? string.Empty;
		var resultadoAlteracao = await Mediator.Send(_updateProduct!);
		if (resultadoAlteracao is { Status: EStatusResponse.Ok, Dado: not null } && resultadoAlteracao.Dado.Id != Guid.Empty)
		{
			NavManager.NavigateTo($"detalhes-produto?id={resultadoAlteracao.Dado.Id}");
		}
		else
		{
			_ocorreuErro = true;
			_mensagemErro = "Não foi possível atualizar o produto. Tente novamente.";
		}

		_carregando = false;
	}

	public void Dispose()
	{
		if (_editContext is not null)
		{
			_editContext.OnValidationRequested -= HandleValidationRequested;
		}
	}
}