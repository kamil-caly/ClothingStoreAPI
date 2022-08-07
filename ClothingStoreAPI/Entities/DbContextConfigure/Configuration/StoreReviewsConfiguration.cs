using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStoreAPI.Entities.DbContextConfigure.Configuration
{
    public class StoreReviewsConfiguration : IEntityTypeConfiguration<StoreReview>
    {
        public void Configure(EntityTypeBuilder<StoreReview> builder)
        {
            builder.Property(r => r.Rating)
               .IsRequired();
        }
    }
}
