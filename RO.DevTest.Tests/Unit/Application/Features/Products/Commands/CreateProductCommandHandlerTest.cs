using BE.Application.Contracts.Persistance.Repositories;
using BE.Application.Features.Products.Commands.CreateProductCommand;
using FluentAssertions;
using Moq;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Tests.Unit.Application.Features.Products.Commands;

public class CreateProductCommandHandlerTest
{
    private readonly Mock<IProductsRepository> _repositoryMock = new();
    private readonly CreateProductCommandHandler _sut;

    public CreateProductCommandHandlerTest()
    {
        _sut = new(_repositoryMock.Object);
    }
    
    [Fact(DisplayName = "When product request is valid, should create product.")]
    public async Task Handle_WhenProductRequestIsValid_ShouldCreateProduct()
    {
        var command = new CreateProductCommand
        {
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

        var productEntity = command.ToEntity();
        
        _repositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(productEntity);
        
        var result = await _sut.Handle(command, CancellationToken.None);
        _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once());
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);
        result.Description.Should().Be(command.Description);
        result.Price.Should().Be(command.Price);
        result.CreatedBy.Should().Be(command.CreatedBy);
    }
    
    [Fact(DisplayName = "When product request is invalid, should throw BadRequestException.")]
    public async Task Handle_WhenProductRequestIsInvalid_ShouldThrowBadRequestException()
    {
        var command = new CreateProductCommand
        {
            Name = null, // Invalid name
            Description = "Test Description",
            Price = -100.00m,
            ImageUrl = "http://example.com/image.jpg",
            Category = "Test Category",
            Brand = "Test Brand",
            Stock = 10,
            IsActive = true,
            CreatedBy = "TestUser"
        };

        Func<Task> act = async () => await _sut.Handle(command, CancellationToken.None);
        
        await act.Should().ThrowAsync<BadRequestException>();
    }
}