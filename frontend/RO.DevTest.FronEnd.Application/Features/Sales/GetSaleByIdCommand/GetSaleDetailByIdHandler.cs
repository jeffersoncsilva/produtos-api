using MediatR;
using System.Text.Json.Serialization;
using System.Text.Json;
using RO.DevTest.FrontEnd.ViewModels.Exceptions;

namespace RO.DevTest.FronEnd.Application.Features.Sales.GetSaleByIdCommand;

public class GetSaleDetailByIdHandler(IHttpClientFactory httpFactory) : IRequestHandler<GetSaleDetailByIdRequest, GetSaleDetailByIdResponse?>
{
	public async Task<GetSaleDetailByIdResponse?> Handle(GetSaleDetailByIdRequest request, CancellationToken ct)
	{
		try
		{
			var httpClient = httpFactory.CreateClient(HttpConfiguration.HttpClientName);

			var result = await httpClient.GetAsync($"api/sales/{request.Id}", ct);

			result.EnsureSuccessStatusCode();

			JsonSerializerOptions op = new()
			{
				ReferenceHandler = ReferenceHandler.Preserve
			};

			var obj = await JsonSerializer.DeserializeAsync<GetSaleDetailByIdResponse>(await result.Content.ReadAsStreamAsync(), op, cancellationToken: ct);
			return obj;
		}
		catch (Exception ex)
		{
			throw new BadRequestException(System.Net.HttpStatusCode.InternalServerError, ex.Message);
		}
	}
}
