using System.Net.Http.Headers;
using MediatR;
using System.Text.Json;
using FE.Application.Contracts;
using FE.ViewModels.Exceptions;
using static FE.Application.JsonOptionsSerialize;

namespace FE.Application.Features.Products.GetProductByIdCommand;

public class GetProductByIdHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService tokenService) : IRequestHandler<GetProductByIdCommandRequest, GetProductByIdResponse?>
{
	public async Task<GetProductByIdResponse?> Handle(GetProductByIdCommandRequest request, CancellationToken ct)
	{
		try
		{
			var httpClient = httpFactory.CreateClient(HttpConfiguration.HttpClientName);
			var token = await tokenService.GetTokenAsync(ct);
			var uri = $"api/products/{request.Id}";
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var result = await httpClient.GetAsync(uri, ct);

			result.EnsureSuccessStatusCode();

			var obj = await JsonSerializer.DeserializeAsync<GetProductByIdResponse>(await result.Content.ReadAsStreamAsync(ct), JsonOptions, cancellationToken: ct);
			return obj;
		}
		catch(Exception ex)
		{
			throw new BadRequestException(System.Net.HttpStatusCode.InternalServerError, ex.Message);
		}
	}
}
