using MediatR;

namespace RO.DevTest.Application.Features.Products.Commands.DeleteProductCommand;

public class DeleteProductCommandRequest(Guid id) : IRequest<DeleteProductResult>
{
    public Guid Id { get; init; } = id;
}