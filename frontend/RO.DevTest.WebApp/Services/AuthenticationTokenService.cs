using Blazored.LocalStorage;
using FE.Application.Contracts;

namespace RO.DevTest.WebApp.Services;

public class AuthenticationTokenService(ILocalStorageService localStorage) : IAuthenticationTokenService
{
	public async Task<string?> GetTokenAsync(CancellationToken ct = default)
	{
		return await localStorage.GetItemAsync<string>(AuthTokenString.TokenName, ct);
	}

	public async ValueTask RemoveTokenAsync(CancellationToken ct = default)
	{
		await localStorage.RemoveItemAsync(AuthTokenString.TokenName, ct);
	}

	public async ValueTask SetTokenAsync(string token, CancellationToken ct = default)
	{
		await localStorage.SetItemAsync(AuthTokenString.TokenName, token, ct);
	}
}