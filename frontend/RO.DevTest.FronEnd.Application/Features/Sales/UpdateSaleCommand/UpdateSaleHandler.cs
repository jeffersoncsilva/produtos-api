using MediatR;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using RO.DevTest.FrontEnd.ViewModels.Exceptions;

namespace RO.DevTest.FronEnd.Application.Features.Sales.UpdateSaleCommand;

public class UpdateSaleHandler(IHttpClientFactory httpFactory) : IRequestHandler<UpdateSaleRequest, UpdateSaleResult?>
{
	public async Task<UpdateSaleResult?> Handle(UpdateSaleRequest request, CancellationToken ct)
	{
		try
		{
			var httpClient = httpFactory.CreateClient(HttpConfiguration.HttpClientName);

			using StringContent jsonContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

			var result = await httpClient.PutAsync("api/sales", jsonContent, ct);

			result.EnsureSuccessStatusCode();

			JsonSerializerOptions op = new()
			{
				ReferenceHandler = ReferenceHandler.Preserve
			};

			var obj = await JsonSerializer.DeserializeAsync<UpdateSaleResult>(await result.Content.ReadAsStreamAsync(), op, cancellationToken: ct);
			return obj;
		}
		catch (Exception ex)
		{
			throw new BadRequestException(System.Net.HttpStatusCode.InternalServerError, ex.Message);
		}
	}
}
