using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Repositories;

public class SaleRepository(DefaultContext db) : BaseRepository<Sale>(db),ISaleRepository
{
    public async Task<IReadOnlyList<Sale>> GetPagedSalesAsync(int page, int size, CancellationToken ct)
    {
        return await db.Sales.AsNoTracking().Skip(page * size).Take(size).Include(p => p.Itens).ToListAsync(ct);
    }
}