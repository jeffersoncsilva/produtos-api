
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BE.Domain.Entities;

namespace BE.Persistence.EntitiesConfigurations;

public sealed class SaleItemComfiguration : IEntityTypeConfiguration<SaleItem>
{
	public void Configure(EntityTypeBuilder<SaleItem> b)
	{
		b.ToTable("SaleItens");

		b.HasKey(s => s.Id);

		b.Property(s => s.Quantity).IsRequired();

		b.Property(s => s.Price).IsRequired().HasColumnType("money");

		b.HasOne(i => i.Sale)
			.WithMany(s => s.Itens)
			.HasForeignKey(i => i.SaleId)
			.HasPrincipalKey(v => v.Id);

		b.HasOne(s => s.Product)
			.WithMany()
			.HasForeignKey(s => s.ProductId)
			.HasPrincipalKey(p => p.Id);
	}
}
