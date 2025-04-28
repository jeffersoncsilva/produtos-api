using MediatR;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using FE.Application.Contracts;
using static FE.Application.JsonOptionsSerialize;

namespace FE.Application.Features.Login.RealizarLoginCommand;

public class LoginHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService authService) : IRequestHandler<LoginRequest, LoginResponse?>
{
	public async Task<LoginResponse?> Handle(LoginRequest? request, CancellationToken ct)
	{
		if (request is null)
			return null;

		try
		{
			using StringContent jsonContent = new(JsonSerializer.Serialize(request, JsonOptions), Encoding.UTF8, "application/json");

			var httpClient = httpFactory.CreateClient(HttpConfiguration.HttpClientName);
			var resultado = await httpClient.PostAsync("api/auth", jsonContent, ct);
			resultado.EnsureSuccessStatusCode();
			var obj = await JsonSerializer.DeserializeAsync<LoginResponse>(await resultado.Content.ReadAsStreamAsync(ct), JsonOptions, ct);
			
			if ((obj?.Success ?? false) && !string.IsNullOrWhiteSpace(obj.AccessToken))
			{
				await authService.SetTokenAsync(obj.AccessToken, ct);
			}

			return obj;
		}
		catch (Exception ex)
		{
			return null;
		}
	}
}