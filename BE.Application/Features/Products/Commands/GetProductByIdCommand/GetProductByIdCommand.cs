using MediatR;

namespace BE.Application.Features.Products.Commands.GetProductByIdCommand;

public class GetProductByIdCommand(Guid id) : IRequest<GetProductByIdResult>
{
    public Guid Id { get; init; } = id;
}