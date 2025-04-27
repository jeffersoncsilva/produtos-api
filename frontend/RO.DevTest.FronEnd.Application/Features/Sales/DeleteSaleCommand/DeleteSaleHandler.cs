using MediatR;
using System.Text.Json.Serialization;
using System.Text.Json;
using RO.DevTest.FrontEnd.ViewModels.Exceptions;

namespace RO.DevTest.FronEnd.Application.Features.Sales.DeleteSaleCommand;

public sealed class DeleteSaleHandler(IHttpClientFactory httpFactory) : IRequestHandler<DeleteSaleRequest, DeleteSaleResponse?>
{
	public async Task<DeleteSaleResponse?> Handle(DeleteSaleRequest request, CancellationToken ct)
	{
		try
		{
			var httpClient = httpFactory.CreateClient(HttpConfiguration.HttpClientName);

			var result = await httpClient.DeleteAsync($"api/sales/{request.Id}", ct);

			result.EnsureSuccessStatusCode();

			JsonSerializerOptions op = new()
			{
				ReferenceHandler = ReferenceHandler.Preserve
			};

			var obj = await JsonSerializer.DeserializeAsync<DeleteSaleResponse>(await result.Content.ReadAsStreamAsync(), op, cancellationToken: ct);
			return obj;
		}
		catch (Exception ex)
		{
			throw new BadRequestException(System.Net.HttpStatusCode.InternalServerError, ex.Message);
		}
	}
}
