using MediatR;
using RO.DevTest.FronEnd.Application.Features.Products.CreateProductCommand;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using RO.DevTest.FrontEnd.ViewModels.Exceptions;

namespace RO.DevTest.FronEnd.Application.Features.Products.UpdateProductCommand;

public class UpdateProductCommandHandler(IHttpClientFactory httpFactory) : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse?>
{
	public async Task<UpdateProductCommandResponse?> Handle(UpdateProductCommandRequest request, CancellationToken ct)
	{
		try
		{
			var httpClient = httpFactory.CreateClient(HttpConfiguration.HttpClientName);

			using StringContent jsonContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

			var result = await httpClient.PutAsync("api/products", jsonContent, ct);

			result.EnsureSuccessStatusCode();

			JsonSerializerOptions op = new()
			{
				ReferenceHandler = ReferenceHandler.Preserve
			};

			var obj = await JsonSerializer.DeserializeAsync<UpdateProductCommandResponse>(await result.Content.ReadAsStreamAsync(), op, cancellationToken: ct);
			return obj;
		}
		catch (Exception ex)
		{
			throw new BadRequestException(System.Net.HttpStatusCode.InternalServerError, ex.Message);
		}
	}
}
