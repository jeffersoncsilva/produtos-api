using System.Net.Http.Headers;
using MediatR;
using System.Text.Json;
using FE.Application.Contracts;
using static FE.Application.JsonOptionsSerialize;

namespace FE.Application.Features.Products.GetProductsCommand;

public class GetProductCommandHandler(IHttpClientFactory factory, IAuthenticationTokenService tokenService) : IRequestHandler<GetProductCommandRequest, GetProductCommandResponse?>
{
	public async Task<GetProductCommandResponse?> Handle(GetProductCommandRequest request, CancellationToken cancellationToken)
	{
		try
		{
			var uri = $"api/products?page={request.Page}&pageSize={request.Size}";
			var token = await tokenService.GetTokenAsync(cancellationToken);
			var httpClient = factory.CreateClient(HttpConfiguration.HttpClientName);
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var resultado = await httpClient.GetAsync(uri, cancellationToken);
			resultado.EnsureSuccessStatusCode();

			var obj = await JsonSerializer.DeserializeAsync<GetProductCommandResponse>(await resultado.Content.ReadAsStreamAsync(cancellationToken), JsonOptions, cancellationToken);
			return obj;
		}
		catch(Exception ex)
		{
			return null;
		}
	}
}
