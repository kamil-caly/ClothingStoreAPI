using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStoreAPI.Entities.DbContextConfigure.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.IsBought)
                .HasDefaultValue(false);

            builder.Property(o => o.CreatedOrderDate)
                .HasDefaultValueSql("getutcdate()");

            builder.Property(o => o.ProductName)
                .IsRequired();

            builder.Property(o => o.ProductPrice)
               .IsRequired();

            builder.Property(o => o.ProductSize)
               .IsRequired();

            builder.Property(o => o.ProductGender)
               .IsRequired();

        }
    }
}
