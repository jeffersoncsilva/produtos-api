using FE.Application.Features.Products.GetProductByIdCommand;
using FE.ViewModels;
using FE.ViewModels.Features.Product;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace FE.WebApp.Pages.Produtos;

public partial class DetalhesProduto
{
	[Inject] public IMediator Mediator { get; set; } = default!;

	[Parameter]
	[SupplyParameterFromQuery(Name = "id")]
	public Guid Id { get; set; }

	private ProductCompleteViewModel? _product;
	private bool _carregando;
	private bool _erroAoCarregarProduto;

	protected override async Task OnInitializedAsync()
	{
		await CarregaDadosProduto();
		await base.OnInitializedAsync();
	}

	private async ValueTask CarregaDadosProduto()
	{
		_carregando = true;
		var productResult = await Mediator.Send(new GetProductByIdCommandRequest(Id));

		if (productResult.Status == EStatusResponse.Ok && productResult.Dado is not null)
		{
			_product = productResult.Dado.ToViewModel();
		}
		else
		{
			_erroAoCarregarProduto = true;
		}

		_carregando = false;
	}
}