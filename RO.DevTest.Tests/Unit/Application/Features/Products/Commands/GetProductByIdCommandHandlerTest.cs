using System.Linq.Expressions;
using BE.Application.Contracts.Persistance.Repositories;
using BE.Application.Features.Products.Commands.GetProductByIdCommand;
using FluentAssertions;
using Moq;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Tests.Unit.Application.Features.Products.Commands;

public class GetProductByIdCommandHandlerTest
{
    private readonly Mock<IProductsRepository> _repositoryMock = new();
    private readonly GetProductByIdCommandHandler _sut;

    public GetProductByIdCommandHandlerTest()
    {
        _sut = new GetProductByIdCommandHandler(_repositoryMock.Object);
    }
    
    [Fact(DisplayName = "When product ID is valid, should return product.")]
    public async Task Handle_WhenProductIdIsValid_ShouldReturnProduct()
    {
        var command = new GetProductByIdCommand(Guid.NewGuid());
        var product = new Product
        {
            Id = command.Id,
            Name = "Test Product",
            Description = "Test Description",
            Price = 100.00m,
            ImageUrl = "http://example.com/image.jpg",
            Category = "Test Category",
            Brand = "Test Brand",
            Stock = 10,
            IsActive = true,
            CreatedBy = "TestUser"
        };

        _repositoryMock.Setup(repo => repo.GetProductAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);
        
        var result = await _sut.Handle(command, CancellationToken.None);
        
        _repositoryMock.Verify(repo => repo.GetProductAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>()), Times.Once());
        result.Should().NotBeNull();
        result.Name.Should().Be(product.Name);
        result.Description.Should().Be(product.Description);
        result.Price.Should().Be(product.Price);
        result.ImageUrl.Should().Be(product.ImageUrl);
        result.Category.Should().Be(product.Category);
        result.Brand.Should().Be(product.Brand);
        result.Stock.Should().Be(product.Stock);
        result.IsActive.Should().Be(product.IsActive);
        result.CreatedBy.Should().Be(product.CreatedBy);
    }
    
    [Fact(DisplayName = "When product ID is invalid, should throw BadRequestException.")]
    public async Task Handle_WhenProductIdIsInvalid_ShouldThrowBadRequestException()
    {
        var command = new GetProductByIdCommand(Guid.NewGuid());
        
        _repositoryMock.Setup(repo => repo.GetProductAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product)null);
        
        Func<Task> act = async () => await _sut.Handle(command, CancellationToken.None);
        
        await act.Should().ThrowAsync<BadRequestException>();
    }
}