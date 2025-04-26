using MediatR;
using RO.DevTest.FronEnd.Application.Features.Products.CreateProductCommand;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using RO.DevTest.FrontEnd.ViewModels.Exceptions;

namespace RO.DevTest.FronEnd.Application.Features.Products.GetProductByIdCommand;

public class GetProductByIdCommandHandler(IHttpClientFactory httpFactory) : IRequestHandler<GetProductByIdCommandRequest, GetProductByIdResponse?>
{
	public async Task<GetProductByIdResponse?> Handle(GetProductByIdCommandRequest request, CancellationToken ct)
	{
		try
		{
			var httpClient = httpFactory.CreateClient(HttpConfiguration.HttpClientName);
			var uri = $"api/products/{request.Id}";
			var result = await httpClient.GetAsync(uri, ct);

			result.EnsureSuccessStatusCode();

			JsonSerializerOptions op = new()
			{
				ReferenceHandler = ReferenceHandler.Preserve
			};

			var obj = await JsonSerializer.DeserializeAsync<GetProductByIdResponse>(await result.Content.ReadAsStreamAsync(), op, cancellationToken: ct);
			return obj;
		}
		catch(Exception ex)
		{
			throw new BadRequestException(System.Net.HttpStatusCode.InternalServerError, ex.Message);
		}
	}
}
