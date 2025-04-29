using FE.Application.Features.Login.RealizarLoginCommand;
using FE.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace FE.WebApp.Pages.Login;

public partial class Login : IDisposable
{
	[Inject] public IMediator Mediator { get; set; } = default!;
	[Inject] public NavigationManager NavManager { get; set; } = default!;
	[Inject] public AuthenticationStateProvider AuthProvider { get; set; } = default!;

	private LoginRequest? _login;
	private EditContext? _editContext;
	private ValidationMessageStore? _messageStore;
	private bool _carregando;
	private bool _falhaLogin;

	protected override void OnInitialized()
	{
		_login = new LoginRequest();
		_editContext = new EditContext(_login);
		_editContext.OnValidationRequested += HandleValidationRequested;
		_messageStore = new ValidationMessageStore(_editContext);
		base.OnInitialized();
	}

	private void HandleValidationRequested(object? sender, ValidationRequestedEventArgs args)
	{
		_messageStore?.Clear();

		if (string.IsNullOrWhiteSpace(_login?.Email))
			_messageStore?.Add(() => _login!.Email ?? string.Empty, "Preencha o campo com o login.");

		if (string.IsNullOrWhiteSpace(_login?.Senha))
			_messageStore?.Add(() => _login!.Senha ?? string.Empty, "Senha é obrigatória.");
	}

	private async Task RealizaLogin()
	{
		_carregando = true;
		var resultado = await Mediator.Send(_login!);
		if (resultado is { Status: EStatusResponse.Ok, Dado.Success: true } && !string.IsNullOrWhiteSpace(resultado.Dado.AccessToken))
		{
			((ApiAuthenticationStateProvider)AuthProvider).MarkUserAsAuthenticated(resultado.Dado.AccessToken!);
			NavManager.NavigateTo("vendas");
		}
		else
		{
			_falhaLogin = true;
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