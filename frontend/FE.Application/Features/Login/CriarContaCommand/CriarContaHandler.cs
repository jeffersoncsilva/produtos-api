using MediatR;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;

namespace FE.Application.Features.Login.CriarContaCommand;

public class CriarContaHandler(IHttpClientFactory httpFactory) : IRequestHandler<NovaContaRequest, NovaContaResponse?>
{
	public async Task<NovaContaResponse?> Handle(NovaContaRequest? request, CancellationToken ct)
	{
		if (request is null)
			return null;

		try
		{
			using StringContent jsonContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
			var httpClient = httpFactory.CreateClient("ApiDevTest");
			var resultado = await httpClient.PostAsync("api/user", jsonContent, ct);
			resultado.EnsureSuccessStatusCode();
			JsonSerializerOptions op = new()
			{
				ReferenceHandler = ReferenceHandler.Preserve
			};
			var obj = await JsonSerializer.DeserializeAsync<NovaContaResponse>(await resultado.Content.ReadAsStreamAsync(ct), op, ct);


			return obj;
		}
		catch (Exception ex)
		{
			return null;
		}
	}
}