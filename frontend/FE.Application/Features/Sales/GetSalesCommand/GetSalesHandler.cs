using System.Net.Http.Headers;
using MediatR;
using System.Text.Json;
using FE.Application.Contracts;
using static FE.Application.JsonOptionsSerialize;

namespace FE.Application.Features.Sales.GetSalesCommand;

public class GetSalesHandler(IHttpClientFactory factory, IAuthenticationTokenService tokenService) : IRequestHandler<GetSalesRequest, GetSalesResponse?>
{
	public async Task<GetSalesResponse?> Handle(GetSalesRequest request, CancellationToken ct)
	{
		try
		{
			var uri = $"api/sales?page={request.Page}&pageSize={request.Size}";
			var httpClient = factory.CreateClient(HttpConfiguration.HttpClientName);
			var token = await tokenService.GetTokenAsync(ct);
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 
			var resultado = await httpClient.GetAsync(uri, ct);
			resultado.EnsureSuccessStatusCode();
			var obj = await JsonSerializer.DeserializeAsync<GetSalesResponse>(await resultado.Content.ReadAsStreamAsync(ct), JsonOptions, ct);
			return obj;
		}
		catch (Exception ex)
		{
			return null;
		}
	}
}
