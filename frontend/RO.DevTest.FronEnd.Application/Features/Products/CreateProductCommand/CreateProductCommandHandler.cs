using MediatR;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using RO.DevTest.FrontEnd.ViewModels.Exceptions;

namespace RO.DevTest.FronEnd.Application.Features.Products.CreateProductCommand;

public sealed class CreateProductCommandHandler(IHttpClientFactory httpFactory) : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse?>
{
	public async Task<CreateProductCommandResponse?> Handle(CreateProductCommandRequest request, CancellationToken ct)
	{
		try
		{
			var httpClient = httpFactory.CreateClient(HttpConfiguration.HttpClientName);

			using StringContent jsonContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

			var result = await httpClient.PostAsync("api/products", jsonContent, ct);

			if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
			{
				var msg = await result.Content.ReadAsStringAsync();
				throw new BadRequestException(result.StatusCode, msg);
			}
			else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{

			}
			else if (result.StatusCode == System.Net.HttpStatusCode.InternalServerError)
			{

			}

			JsonSerializerOptions op = new()
			{
				ReferenceHandler = ReferenceHandler.Preserve
			};

			var obj = await JsonSerializer.DeserializeAsync<CreateProductCommandResponse>(await result.Content.ReadAsStreamAsync(), op, cancellationToken: ct);
			return obj;
		}
		catch (Exception ex)
		{
			throw new BadRequestException(System.Net.HttpStatusCode.InternalServerError, ex.Message);
		}
	}
}
