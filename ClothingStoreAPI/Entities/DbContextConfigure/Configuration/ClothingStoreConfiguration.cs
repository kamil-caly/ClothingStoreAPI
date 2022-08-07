using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStoreAPI.Entities.DbContextConfigure.Configuration
{
    public class ClothingStoreConfiguration : IEntityTypeConfiguration<ClothingStore>
    {
        public void Configure(EntityTypeBuilder<ClothingStore> builder)
        {
            builder.Property(s => s.Name)
                .IsRequired();

            builder.Property(s => s.ContactEmail)
                 .IsRequired();

            builder.Property(s => s.CreatedDate)
                 .HasDefaultValueSql("getutcdate()");

            builder.Property(s => s.Incame)
                 .HasColumnType("decimal(15,2)");

            builder.Property(s => s.ContactNumber)
                 .IsRequired();

            builder.HasOne(s => s.Address)
                .WithOne(a => a.Store)
                .HasForeignKey<ClothingStore>(s => s.AddressId);

            builder.HasOne(s => s.Owner)
                .WithOne(a => a.Store)
                .HasForeignKey<ClothingStore>(s => s.OwnerId);

            builder.HasMany(s => s.StoreReviews)
                .WithOne(r => r.Store)
                .HasForeignKey(r => r.StoreId);

            builder.HasMany(s => s.Products)
                .WithOne(p => p.Store)
                .HasForeignKey(p => p.StoreId);
        }
    }
}
