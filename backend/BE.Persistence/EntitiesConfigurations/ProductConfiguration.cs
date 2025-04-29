using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BE.Domain.Entities;

namespace BE.Persistence.EntitiesConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> b)
    {
        b.ToTable("Products");
        b.HasKey(p => p.Id);

        b.Property(p => p.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");

        b.Property(p => p.Description)
            .IsRequired()
            .HasColumnType("varchar(1024)");

        b.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("money");
        
        b.Property(p => p.ImageUrl)
            .IsRequired()
            .HasColumnType("varchar(512)");
        
        b.Property(p => p.Category)
            .IsRequired()
            .HasColumnType("varchar(64)");

        b.Property(p => p.Brand)
            .IsRequired()
            .HasColumnType("varchar(128)");
        
        b.Property(p => p.CreatedBy)
            .IsRequired(false)
            .HasColumnType("varchar(100)");

        b.Property(p => p.ModifiedBy)
            .IsRequired(false)
            .HasColumnType("varchar(100)");

        b.Property(p => p.IsRemovedFromStock)
            .IsRequired()
            .HasDefaultValue(false);
    }
}