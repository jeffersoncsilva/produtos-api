using System.Text.Json.Serialization;
using MediatR;

namespace BE.Application.Features.Sales.Commands.GetSaleByIdCommand;

public class GetSaleByIdCommandRequest(Guid id) : IRequest<GetSaleByIdCommandResponse?>
{
    [JsonPropertyName("id")] public Guid Id { get; init; } = id;
}