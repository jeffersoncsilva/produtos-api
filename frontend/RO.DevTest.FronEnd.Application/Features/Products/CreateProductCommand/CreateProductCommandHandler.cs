using System.Net.Http.Headers;
using MediatR;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using RO.DevTest.FronEnd.Application.Contracts;
using RO.DevTest.FrontEnd.ViewModels.Exceptions;
using static RO.DevTest.FronEnd.Application.JsonOptionsSerialize;

namespace RO.DevTest.FronEnd.Application.Features.Products.CreateProductCommand;

public sealed class CreateProductCommandHandler(IHttpClientFactory httpFactory, IAuthenticationTokenService tokenService) : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse?>
{
	public async Task<CreateProductCommandResponse?> Handle(CreateProductCommandRequest request, CancellationToken ct)
	{
		try
		{
			var httpClient = httpFactory.CreateClient(HttpConfiguration.HttpClientName);

			using StringContent jsonContent = new(JsonSerializer.Serialize(request, JsonOptions), Encoding.UTF8, "application/json");
			var token = await tokenService.GetTokenAsync(ct);
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var result = await httpClient.PostAsync("api/products", jsonContent, ct);

			if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
			{
				var msg = await result.Content.ReadAsStringAsync(ct);
				throw new BadRequestException(result.StatusCode, msg);
			}
			else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{

			}
			else if (result.StatusCode == System.Net.HttpStatusCode.InternalServerError)
			{

			}

			var obj = await JsonSerializer.DeserializeAsync<CreateProductCommandResponse>(await result.Content.ReadAsStreamAsync(ct), JsonOptions, cancellationToken: ct);
			return obj;
		}
		catch (Exception ex)
		{
			throw new BadRequestException(System.Net.HttpStatusCode.InternalServerError, ex.Message);
		}
	}
}
