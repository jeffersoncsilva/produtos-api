using BE.Domain.Entities;

namespace BE.Application.Contracts.Persistance.Repositories;

public interface ISaleRepository : IBaseRepository<Sale>
{
	Task<IReadOnlyList<Sale>> GetPagedSalesAsync(int page, int size, CancellationToken ct);
	Task<Sale?> GetSaleById(Guid id, CancellationToken ct);
}