using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppP416.Models;

namespace ShopAppP416.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(50);
            builder.Property(p => p.SalePrice)
                .IsRequired(true)
                .HasDefaultValue(10);
            builder.Property(p => p.CostPrice)
                .IsRequired(true)
                .HasDefaultValue(10);
            builder.Property(p=>p.CreatedAt).HasDefaultValue(DateTime.UtcNow);
        }
    }
}
