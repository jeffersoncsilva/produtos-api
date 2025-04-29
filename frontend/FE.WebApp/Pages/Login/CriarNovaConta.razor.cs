using FE.Application.Features.Login.CriarContaCommand;
using FE.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FE.WebApp.Pages.Login;

public partial class CriarNovaConta : IDisposable
{
	[Inject] public IMediator Mediator { get; set; } = default!;
	[Inject] public NavigationManager NavManager { get; set; } = default!;

	private NovaContaRequest? _novaConta;
	private EditContext? _editContext;
	private ValidationMessageStore? _messageStore;

	private bool _carregando;
	private bool _ocorreuErro;
	private string? _mensagemErro;

	protected override void OnInitialized()
	{
		_novaConta = new NovaContaRequest();
		_editContext = new EditContext(_novaConta);
		_editContext.OnValidationRequested += HandleValidationRequested;
		_messageStore = new ValidationMessageStore(_editContext);
		base.OnInitialized();
	}

	private void HandleValidationRequested(object? sender, ValidationRequestedEventArgs args)
	{
		_messageStore?.Clear();

		if (string.IsNullOrWhiteSpace(_novaConta?.UserName))
			_messageStore?.Add(() => _novaConta!.Email, "O nome de usuário e obrigatório.");

		if (string.IsNullOrWhiteSpace(_novaConta?.Name))
			_messageStore?.Add(() => _novaConta!.Name, "Nome é obrigatória.");

		if (string.IsNullOrWhiteSpace(_novaConta?.Email))
			_messageStore?.Add(() => _novaConta!.Email, "E-mail e obrigatório.");

		if (string.IsNullOrWhiteSpace(_novaConta?.Password))
			_messageStore?.Add(() => _novaConta!.Password, "Senha é obrigatório.");

		if (string.IsNullOrWhiteSpace(_novaConta?.PasswordConfirmation))
			_messageStore?.Add(() => _novaConta!.PasswordConfirmation, "Confirmação de senha é obrigatório");

		if (String.CompareOrdinal(_novaConta?.Password, _novaConta?.PasswordConfirmation) != 0)
		{
			_messageStore?.Add(() => _novaConta!.PasswordConfirmation, "Contas não conferem.");
		}
	}

	private async Task CriarConta()
	{
		_carregando = true;
		_ocorreuErro = false;
		StateHasChanged();

		var resultado = await Mediator.Send(_novaConta!);
		if (resultado is { Status: EStatusResponse.Ok, Dado.Success: true })
		{
			NavManager.NavigateTo("login");
		}
		else
		{
			_ocorreuErro = true;
			_mensagemErro = "Ocorreu um erro ao fazer o cadastro. Recarregue a página e tente novamente.";
		}

		_carregando = false;
		StateHasChanged();
	}

	public void Dispose()
	{
		if(_editContext is not null)
			_editContext.OnValidationRequested -= HandleValidationRequested;

	}
}