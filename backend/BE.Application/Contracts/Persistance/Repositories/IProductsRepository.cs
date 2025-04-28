using System.Linq.Expressions;
using BE.Domain.Entities;

namespace BE.Application.Contracts.Persistance.Repositories;

public interface IProductsRepository : IBaseRepository<Product>
{
    Task<IReadOnlyList<Product>> GetPagedProducts(int page, int pageSize, CancellationToken ct);
    Task<Product?> GetProductAsync(Expression<Func<Product, bool>> predicate, CancellationToken ct);
}