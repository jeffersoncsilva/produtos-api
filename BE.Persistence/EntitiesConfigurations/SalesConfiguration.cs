using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BE.Domain.Entities;

namespace BE.Persistence.EntitiesConfigurations;

public class SalesConfiguration : IEntityTypeConfiguration<Sale>
{
	public void Configure(EntityTypeBuilder<Sale> b)
	{
		b.ToTable("Sales");
		b.HasKey(s => s.Id);

		b.Property(s => s.Observation)
			.IsRequired(false)
			.HasColumnType("varchar(2048)");

		b.Property(s => s.Descount)
			.IsRequired()
			.HasColumnType("money");

		b.Property(s => s.Price)
			.IsRequired()
			.HasColumnType("money");

		b.HasMany(s => s.Itens)
			.WithOne(e => e.Sale)
			.HasForeignKey(i => i.SaleId)
			.HasPrincipalKey(v => v.Id);
	}
}
