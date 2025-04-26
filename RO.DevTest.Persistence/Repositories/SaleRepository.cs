using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Repositories;

public class SaleRepository(DefaultContext db) : BaseRepository<Sale>(db),ISaleRepository
{
    public async Task<IReadOnlyList<Sale>> GetPagedSalesAsync(int page, int size, CancellationToken ct)
    {
        return await db.Sales
            .AsNoTracking()
            .Distinct()
            .OrderBy(s => s.Id)
            .Skip(page * size)
            .Take(size)
            .Include(p => p.Itens)!
            .ThenInclude(i => i.Product)
            .ToListAsync(ct);
    }

    public async Task<Sale?> GetSaleById(Guid id, CancellationToken ct)
    {
        return await db.Sales
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Include(s => s.Itens)!
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(ct);
    }
}