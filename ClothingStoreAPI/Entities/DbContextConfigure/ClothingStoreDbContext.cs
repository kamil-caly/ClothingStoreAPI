using Microsoft.EntityFrameworkCore;

namespace ClothingStoreAPI.Entities.DbContextConfigure
{
    public class ClothingStoreDbContext : DbContext
    {
        public ClothingStoreDbContext(DbContextOptions<ClothingStoreDbContext> options)
            : base(options) { }
        
        public DbSet<ClothingStore> ClothingStores { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<StoreReview> StoreReviews { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
