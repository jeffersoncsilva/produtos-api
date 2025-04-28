using System.Net.Http.Headers;
using MediatR;
using System.Text.Json;
using System.Text;
using FE.Application.Contracts;
using RO.DevTest.FrontEnd.ViewModels.Exceptions;
using static FE.Application.JsonOptionsSerialize;

namespace FE.Application.Features.Sales.UpdateSaleCommand;

public class UpdateSaleHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService tokenService) : IRequestHandler<UpdateSaleRequest, UpdateSaleResult?>
{
	public async Task<UpdateSaleResult?> Handle(UpdateSaleRequest request, CancellationToken ct)
	{
		try
		{
			var httpClient = httpFactory.CreateClient(HttpConfiguration.HttpClientName);

			using StringContent jsonContent = new(JsonSerializer.Serialize(request, JsonOptions), Encoding.UTF8, "application/json");
			var token = await tokenService.GetTokenAsync(ct);
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var result = await httpClient.PutAsync("api/sales", jsonContent, ct);

			result.EnsureSuccessStatusCode();

			var obj = await JsonSerializer.DeserializeAsync<UpdateSaleResult>(await result.Content.ReadAsStreamAsync(ct), JsonOptions, cancellationToken: ct);
			return obj;
		}
		catch (Exception ex)
		{
			throw new BadRequestException(System.Net.HttpStatusCode.InternalServerError, ex.Message);
		}
	}
}
