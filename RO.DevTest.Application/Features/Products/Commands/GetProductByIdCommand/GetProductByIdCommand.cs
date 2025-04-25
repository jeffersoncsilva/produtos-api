using MediatR;

namespace RO.DevTest.Application.Features.Products.Commands.GetProductByIdCommand;

public class GetProductByIdCommand(Guid id) : IRequest<GetProductByIdResult>
{
    public Guid Id { get; init; } = id;
}