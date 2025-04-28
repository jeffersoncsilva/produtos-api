using System.Net.Http.Headers;
using MediatR;
using System.Text.Json;
using System.Text;
using RO.DevTest.FronEnd.Application.Contracts;
using RO.DevTest.FrontEnd.ViewModels.Exceptions;
using static RO.DevTest.FronEnd.Application.JsonOptionsSerialize;

namespace RO.DevTest.FronEnd.Application.Features.Sales.CreateSaleCommand;

public class CreateSaleHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService tokenService) : IRequestHandler<CreateSaleRequest, CreateSaleResponse>
{
	public async Task<CreateSaleResponse> Handle(CreateSaleRequest request, CancellationToken ct)
	{
		try
		{
			var httpClient = httpFactory.CreateClient(HttpConfiguration.HttpClientName);

			using StringContent jsonContent = new(JsonSerializer.Serialize(request, JsonOptions), Encoding.UTF8, "application/json");
			var token = await tokenService.GetTokenAsync(ct);
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var result = await httpClient.PostAsync("api/sales", jsonContent, ct);

			result.EnsureSuccessStatusCode();

			var obj = await JsonSerializer.DeserializeAsync<CreateSaleResponse>(await result.Content.ReadAsStreamAsync(ct), JsonOptions, cancellationToken: ct);
			return obj;
		}
		catch (Exception ex)
		{
			throw new BadRequestException(System.Net.HttpStatusCode.InternalServerError, ex.Message);
		}
	}
}
