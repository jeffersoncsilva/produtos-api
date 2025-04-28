using MediatR;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;

namespace RO.DevTest.FronEnd.Application.Features.Login.RealizarLoginCommand;

public class LoginHandler(IHttpClientFactory httpFactory) : IRequestHandler<LoginRequest, LoginResponse?>
{
	public async Task<LoginResponse?> Handle(LoginRequest? request, CancellationToken ct)
	{
		if (request is null)
			return null;

		try
		{
			using StringContent jsonContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

			var httpClient = httpFactory.CreateClient("ApiDevTest");
			var resultado = await httpClient.PostAsync("api/auth", jsonContent, ct);
			resultado.EnsureSuccessStatusCode();
			JsonSerializerOptions op = new()
			{
				ReferenceHandler = ReferenceHandler.Preserve
			};
			var obj = await JsonSerializer.DeserializeAsync<LoginResponse>(await resultado.Content.ReadAsStreamAsync(ct), op, ct);
			
			if (obj?.Success ?? false)
			{
				// TODO: salvar token de autenticação para requisições posteriores
			}

			return obj;
		}
		catch (Exception ex)
		{
			return null;
		}
	}
}