using MediatR;

namespace RO.DevTest.Application.Features.Sales.Commands.DeleteSaleCommand;

public class DeleteSaleCommandRequest(Guid id) : IRequest<DeleteSaleCommandResponse?>
{
    public Guid Id { get; init; } = id;
}