using MediatR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RO.DevTest.FronEnd.Application.Features.Products.GetProductsCommand;

public class GetProductCommandHandler(IHttpClientFactory factory) : IRequestHandler<GetProductCommandRequest, GetProductCommandResponse?>
{
	public async Task<GetProductCommandResponse?> Handle(GetProductCommandRequest request, CancellationToken cancellationToken)
	{
		try
		{
			var uri = $"api/products?page={request.Page}&pageSize={request.Size}";
			var httpClient = factory.CreateClient("ApiDevTest");
			var resultado = await httpClient.GetAsync(uri);
			resultado.EnsureSuccessStatusCode();
			JsonSerializerOptions op = new()
			{
				ReferenceHandler = ReferenceHandler.Preserve
			};
			var obj = await JsonSerializer.DeserializeAsync<GetProductCommandResponse>(await resultado.Content.ReadAsStreamAsync(), op);
			return obj;
		}
		catch(Exception ex)
		{
			return null;
		}
	}
}
