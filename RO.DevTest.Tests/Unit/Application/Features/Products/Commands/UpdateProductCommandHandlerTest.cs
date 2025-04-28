using System.Linq.Expressions;
using BE.Application.Contracts.Persistance.Repositories;
using BE.Application.Features.Products.Commands.UpdateProductCommand;
using FluentAssertions;
using Moq;
using BE.Domain.Entities;

namespace RO.DevTest.Tests.Unit.Application.Features.Products.Commands;

public class UpdateProductCommandHandlerTest
{
    private readonly Mock<IProductsRepository> _repositoryMock = new();
    private readonly UpdateProductCommandHandler _sut;

    public UpdateProductCommandHandlerTest()
    {
        _sut = new UpdateProductCommandHandler(_repositoryMock.Object);
    }

    [Fact(DisplayName = "When product request is valid, should update product.")]
    public async Task Handle_WhenProductRequestIsValid_ShouldUpdateProduct()
    {
        var command = new UpdateProductCommandRequest()
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            Description = "Test Description",
            Price = 100.00m,
            ImageUrl = "http://example.com/image.jpg",
            Category = "Test Category",
            Brand = "Test Brand",
            Stock = 10,
            IsActive = true,
            ModifiedBy = "TestUser"
        };
        var product = command.ToEntity();
        _repositoryMock.Setup(repo => repo.GetProductAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(product);
        _repositoryMock.Setup(repo => repo.Update(It.IsAny<Product>()));

        var result = await _sut.Handle(command, CancellationToken.None);
        _repositoryMock.Verify(repo => repo.GetProductAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Once());
        result.Should().NotBeNull();
        result.Id.Should().Be(command.Id);
    }
}