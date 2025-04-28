using System.Net.Http.Headers;
using MediatR;
using System.Text.Json;
using RO.DevTest.FronEnd.Application.Contracts;
using RO.DevTest.FrontEnd.ViewModels.Exceptions;
using static RO.DevTest.FronEnd.Application.JsonOptionsSerialize;

namespace RO.DevTest.FronEnd.Application.Features.Sales.GetSaleByIdCommand;

public class GetSaleDetailByIdHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService tokenService) : IRequestHandler<GetSaleDetailByIdRequest, GetSaleDetailByIdResponse?>
{
	public async Task<GetSaleDetailByIdResponse?> Handle(GetSaleDetailByIdRequest request, CancellationToken ct)
	{
		try
		{
			var httpClient = httpFactory.CreateClient(HttpConfiguration.HttpClientName);
			var token = await tokenService.GetTokenAsync(ct);
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var result = await httpClient.GetAsync($"api/sales/{request.Id}", ct);

			result.EnsureSuccessStatusCode();

			var obj = await JsonSerializer.DeserializeAsync<GetSaleDetailByIdResponse>(await result.Content.ReadAsStreamAsync(ct), JsonOptions, cancellationToken: ct);
			return obj;
		}
		catch (Exception ex)
		{
			throw new BadRequestException(System.Net.HttpStatusCode.InternalServerError, ex.Message);
		}
	}
}
