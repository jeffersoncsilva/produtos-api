using MediatR;

namespace RO.DevTest.Application.Features.Sales.Commands.CreateSaleCommand;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    public Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}