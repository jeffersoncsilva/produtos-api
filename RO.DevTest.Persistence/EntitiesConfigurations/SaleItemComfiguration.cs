
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.EntitiesConfigurations;

public sealed class SaleItemComfiguration : IEntityTypeConfiguration<SaleItem>
{
	public void Configure(EntityTypeBuilder<SaleItem> b)
	{
		b.ToTable("SaleItens");

		b.HasKey(s => s.Id);

		b.Property(s => s.Quantity).IsRequired();

		b.Property(s => s.Price).IsRequired().HasColumnType("money");

		b.HasOne(i => i.Sale)
			.WithMany()
			.HasForeignKey(i => i.SaleId)
			.HasPrincipalKey(sa => sa.Id);

		b.HasOne(s => s.Product)
			.WithMany()
			.HasForeignKey(s => s.ProductId)
			.HasPrincipalKey(p => p.Id);
	}
}
