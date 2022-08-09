using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Entities.DbContextConfigure;

namespace ClothingStoreAPI.Seeders
{
    public class UserRoleSeeder
    {
        private readonly ClothingStoreDbContext dbContext;

        public UserRoleSeeder(ClothingStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            if (dbContext.Database.CanConnect())
            {
                if (!dbContext.Roles.Any())
                {
                    dbContext.Roles.AddRange(GetRoles());
                    dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"        
                },

                new Role()
                {
                    Name = "UserPremium"
                },

                new Role()
                {
                    Name = "Manager"
                },

                new Role()
                {
                    Name = "Admin"
                },

            };

            return roles;
        }
    }
}
