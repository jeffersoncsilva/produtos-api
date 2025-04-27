using MediatR;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using RO.DevTest.FrontEnd.ViewModels.Exceptions;

namespace RO.DevTest.FronEnd.Application.Features.Sales.CreateSaleCommand;

public class CreateSaleHandler(IHttpClientFactory httpFactory) : IRequestHandler<CreateSaleRequest, CreateSaleResponse>
{
	public async Task<CreateSaleResponse> Handle(CreateSaleRequest request, CancellationToken ct)
	{
		try
		{
			var httpClient = httpFactory.CreateClient(HttpConfiguration.HttpClientName);

			using StringContent jsonContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

			var result = await httpClient.PostAsync("api/sales", jsonContent, ct);

			result.EnsureSuccessStatusCode();

			JsonSerializerOptions op = new()
			{
				ReferenceHandler = ReferenceHandler.Preserve
			};

			Console.WriteLine($"resultado: {await result.Content.ReadAsStringAsync()}");

			var obj = await JsonSerializer.DeserializeAsync<CreateSaleResponse>(await result.Content.ReadAsStreamAsync(), op, cancellationToken: ct);
			return obj;
		}
		catch (Exception ex)
		{
			throw new BadRequestException(System.Net.HttpStatusCode.InternalServerError, ex.Message);
		}
	}
}
