using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStoreAPI.Entities.DbContextConfigure.Configuration
{
    public class StoreReviewsConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            throw new NotImplementedException();
        }
    }
}
