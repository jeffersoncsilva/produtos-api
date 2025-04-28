using System.Linq.Expressions;
using BE.Application.Contracts.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using BE.Domain.Entities;

namespace BE.Persistence.Repositories;

internal class ProductRepository(DefaultContext db) : BaseRepository<Product>(db), IProductsRepository
{
    public async Task<IReadOnlyList<Product>> GetPagedProducts(int page, int pageSize, CancellationToken ct)
    {
        return await db.Products.AsNoTracking().OrderBy(p => p.Id).Skip(page * pageSize).Take(pageSize).ToListAsync(ct);
    }

    public async Task<Product?> GetProductAsync(Expression<Func<Product, bool>> predicate, CancellationToken ct)
    {
        return await db.Products.Where(predicate).FirstOrDefaultAsync(ct);
    }
}