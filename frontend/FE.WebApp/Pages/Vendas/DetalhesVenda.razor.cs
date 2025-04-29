using FE.Application.Features.Sales.GetSaleByIdCommand;
using FE.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace FE.WebApp.Pages.Vendas;

public partial class DetalhesVenda
{
	[Inject] public IMediator Mediator { get; set; } = default!;

	[Parameter]
	[SupplyParameterFromQuery(Name = "id")]
	public Guid Id { get; set; }

	private GetSaleDetailByIdResponse? _response;
	private bool _carregando;
	private bool _erroAoCarregar;

	protected override async Task OnInitializedAsync()
	{
		await CarregarVendaDetalhe();
		await base.OnInitializedAsync();
	}

	private async ValueTask CarregarVendaDetalhe()
	{
		_carregando = true;
		StateHasChanged();
		var resposta = await Mediator.Send(new GetSaleDetailByIdRequest(Id));
		if(resposta is { Status: EStatusResponse.Ok, Dado: not null })
		{
			_response = resposta.Dado;
		}
		else
		{
			_erroAoCarregar = true;
		}
		_carregando = false;
	}
}