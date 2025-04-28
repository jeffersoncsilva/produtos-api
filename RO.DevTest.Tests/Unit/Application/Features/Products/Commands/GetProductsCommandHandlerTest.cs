using BE.Application.Contracts.Persistance.Repositories;
using BE.Application.Features.Products.Commands.GetProductsCommand;
using Bogus;
using FluentAssertions;
using Moq;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Tests.Unit.Application.Features.Products.Commands;

public class GetProductsCommandHandlerTest
{
    private readonly Mock<IProductsRepository> _repositoryMock = new();
    private readonly GetProductsCommandHandler _sut;

    public GetProductsCommandHandlerTest()
    {
        _sut = new GetProductsCommandHandler(_repositoryMock.Object);
    }
    
    [Fact(DisplayName = "When product ID is valid, should return product.")]
    public async Task Handle_WhenProductIdIsValid_ShouldReturnProduct()
    {
        var command = new GetProductsRequest(1, 5);
        var products = CreateMockResult(5);
        _repositoryMock.Setup(r => r.GetPagedProducts(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(products);

        var result = await _sut.Handle(command, CancellationToken.None);
        
        _repositoryMock.Verify(repo => repo.GetPagedProducts(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once());
        
        result.Should().NotBeNull();
        result.Page.Should().Be(1);
        result.Size.Should().Be(5);
        result.Products.Should().NotBeEmpty();
        result.Products.Count().Should().Be(5);
        result.Products.Should().BeEquivalentTo(products);
    }

    private IReadOnlyList<Product> CreateMockResult(int qtd)
    {
        var faker = new Faker<Product>();
        faker.RuleFor(p => p.Id, f => Guid.NewGuid());
        faker.RuleFor(p => p.Name, f => f.Commerce.ProductName());
        faker.RuleFor(p => p.Description, f => f.Commerce.ProductDescription());
        faker.RuleFor(p => p.Price, f => f.Random.Decimal(0M));
        faker.RuleFor(p => p.ImageUrl, f => f.Internet.Url());
        faker.RuleFor(p => p.Category, f => f.Commerce.Categories(1).FirstOrDefault());
        faker.RuleFor(p => p.Brand, f => f.Commerce.Product());
        faker.RuleFor(p => p.Stock, f => f.Random.Int(0, 100));
        faker.RuleFor(p => p.IsActive, f => true);
        faker.RuleFor(p => p.CreatedOn, f => DateTime.UtcNow);
        faker.RuleFor(p => p.ModifiedOn, f => DateTime.UtcNow);
        faker.RuleFor(p => p.CreatedBy, f => f.Internet.UserName());
        faker.RuleFor(p => p.ModifiedBy, f => f.Internet.UserName());
        faker.RuleFor(p => p.CreatedOn, f => DateTime.UtcNow);

        return faker.Generate(qtd);
    }
}