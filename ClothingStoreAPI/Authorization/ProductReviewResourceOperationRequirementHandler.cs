using ClothingStoreAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ClothingStoreAPI.Authorization
{
    public class ProductReviewResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, ProductReview>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, ProductReview review)
        {
            if (requirement.ResourceOperation == ResourceOperation.Read ||
                requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (review.CreatedById == int.Parse(userId) ||
                context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value == "Admin")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
