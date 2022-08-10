using ClothingStoreAPI.Entities.DbContextConfigure;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ClothingStoreAPI.Authorization
{
    public class MinimumStoreCreatedHandler : AuthorizationHandler<MinimumStoreCreated>
    {
        private readonly ClothingStoreDbContext dbContext;

        public MinimumStoreCreatedHandler(ClothingStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumStoreCreated requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var storeCreatedCount = dbContext
                .ClothingStores
                .Where(s => s.CreatedById == userId).Count();

            if (storeCreatedCount >= requirement.MinimumStore)
            {
                context.Succeed(requirement);
            }
          
            return Task.CompletedTask;
        }
    }
}
