using System.Linq.Expressions;
using BE.Application.Contracts.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using BE.Domain.Entities;

namespace BE.Persistence.Repositories;

internal class ProductRepository(DefaultContext db) : BaseRepository<Product>(db), IProductsRepository
{
    public async Task<IReadOnlyList<Product>> GetPagedProducts(int page, int pageSize, CancellationToken ct)
    {
        return await Context.Products
            .AsNoTracking()
            .Distinct()
            .Where(p => !p.IsRemovedFromStock && !p.IsRemovedFromStock)
            .OrderBy(p => p.Name)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public async Task<Product?> GetProductAsync(Expression<Func<Product, bool>> predicate, CancellationToken ct)
    {
        return await Context.Products.Where(predicate).FirstOrDefaultAsync(ct);
    }

    public async Task<int> GetTotalProducts(CancellationToken ct) => await Context.Products.AsNoTracking().CountAsync(ct);
}