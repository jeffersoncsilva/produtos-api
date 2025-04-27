using MediatR;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace RO.DevTest.FronEnd.Application.Features.Sales.GetSalesCommand;

public class GetSalesHandler(IHttpClientFactory factory) : IRequestHandler<GetSalesRequest, GetSalesResponse?>
{
	public async Task<GetSalesResponse?> Handle(GetSalesRequest request, CancellationToken ct)
	{
		try
		{
			var uri = $"api/sales?page={request.Page}&pageSize={request.Size}";
			var httpClient = factory.CreateClient(HttpConfiguration.HttpClientName);
			var resultado = await httpClient.GetAsync(uri);
			resultado.EnsureSuccessStatusCode();
			JsonSerializerOptions op = new()
			{
				ReferenceHandler = ReferenceHandler.Preserve
			};
			var obj = await JsonSerializer.DeserializeAsync<GetSalesResponse>(await resultado.Content.ReadAsStreamAsync(), op);
			return obj;
		}
		catch (Exception ex)
		{
			return null;
		}
	}
}
