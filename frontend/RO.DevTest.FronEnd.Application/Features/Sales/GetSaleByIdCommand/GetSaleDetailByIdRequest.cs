using MediatR;
using System.Text.Json.Serialization;

namespace RO.DevTest.FronEnd.Application.Features.Sales.GetSaleByIdCommand;

public class GetSaleDetailByIdRequest(Guid id) : IRequest<GetSaleDetailByIdResponse>
{
	[JsonPropertyName("id")]
	public Guid Id { get; init; } = id;
}
