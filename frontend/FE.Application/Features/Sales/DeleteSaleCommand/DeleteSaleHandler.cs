using System.Net.Http.Headers;
using MediatR;
using System.Text.Json;
using FE.Application.Contracts;
using FE.ViewModels.Exceptions;
using static FE.Application.JsonOptionsSerialize;

namespace FE.Application.Features.Sales.DeleteSaleCommand;

public sealed class DeleteSaleHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService tokenService) : IRequestHandler<DeleteSaleRequest, DeleteSaleResponse?>
{
	public async Task<DeleteSaleResponse?> Handle(DeleteSaleRequest request, CancellationToken ct)
	{
		try
		{
			var httpClient = httpFactory.CreateClient(HttpConfiguration.HttpClientName);
			var token = await tokenService.GetTokenAsync(ct);
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var result = await httpClient.DeleteAsync($"api/sales/{request.Id}", ct);

			result.EnsureSuccessStatusCode();

			var obj = await JsonSerializer.DeserializeAsync<DeleteSaleResponse>(await result.Content.ReadAsStreamAsync(ct), JsonOptions, cancellationToken: ct);
			return obj;
		}
		catch (Exception ex)
		{
			throw new BadRequestException(System.Net.HttpStatusCode.InternalServerError, ex.Message);
		}
	}
}
