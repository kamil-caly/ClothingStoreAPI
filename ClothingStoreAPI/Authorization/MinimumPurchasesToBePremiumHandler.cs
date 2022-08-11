using ClothingStoreAPI.Entities.DbContextConfigure;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ClothingStoreAPI.Authorization
{
    public class MinimumPurchasesToBePremiumHandler : AuthorizationHandler<MinimumPurchasesToBePremium>
    {
        private readonly ClothingStoreDbContext dbContext;

        public MinimumPurchasesToBePremiumHandler(ClothingStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumPurchasesToBePremium requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var user = dbContext
                .Users.FirstOrDefault(u => u.Id == userId);

            var basket = dbContext
                .Baskets
                .FirstOrDefault(b => b.CreatedById == user.Id);

            var userOrders = dbContext
                .Orders
                .Where(o => o.IsBought && o.BasketId == basket.Id)
                .ToList();

            var userPurchases = userOrders.Select(o => o.ProductQuantity).Sum();

            if (userPurchases >= requirement.MinimumPurchases)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
