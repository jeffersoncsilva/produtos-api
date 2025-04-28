using BE.Application.Contracts.Persistance.Repositories;
using BE.Application.Features.Products.Commands.DeleteProductCommand;
using FluentAssertions;
using Moq;
using BE.Domain.Entities;

namespace BE.UnitTests.Unit.Application.Features.Products.Commands;

public class DeleteProductCommandHandlerTest
{
    private readonly Mock<IProductsRepository> _repositoryMock = new();
    private readonly DeleteProductCommandHandler _sut;

    public DeleteProductCommandHandlerTest()
    {
        _sut = new DeleteProductCommandHandler(_repositoryMock.Object);
    }
    
    [Fact(DisplayName = "When product ID is valid, should delete product.")]
    public async Task Handle_WhenProductIdIsValid_ShouldDeleteProduct()
    {
        var command = new DeleteProductCommandRequest(Guid.NewGuid());

        var result = await _sut.Handle(command, CancellationToken.None);
        
        _repositoryMock.Verify(repo => repo.Delete(It.IsAny<Product>()), Times.Once());
        
        result.Should().NotBeNull();
    }
}