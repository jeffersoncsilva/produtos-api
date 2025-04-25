namespace RO.DevTest.Application.Features.Products.Commands.GetProductsCommand;

public class GetProductsResult
{
    public IReadOnlyList<RO.DevTest.Domain.Entities.Product>? Products { get; init; }
    public int Page { get; init; }
    public int Size { get; init; }
}
