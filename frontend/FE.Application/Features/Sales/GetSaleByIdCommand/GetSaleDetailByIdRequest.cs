using MediatR;
using System.Text.Json.Serialization;
using FE.ViewModels;

namespace FE.Application.Features.Sales.GetSaleByIdCommand;

public class GetSaleDetailByIdRequest(Guid id) : IRequest<BaseResponse<GetSaleDetailByIdResponse>>
{
	[JsonPropertyName("id")]
	public Guid Id { get; init; } = id;
}
