using ClothingStoreAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ClothingStoreAPI.Authorization
{
    public class StoreReviewResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, StoreReview>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, StoreReview review)
        {
            if (requirement.ResourceOperation == ResourceOperation.Read ||
                requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (review.CreatedById == int.Parse(userId) ||
                context.User?.FindFirst(c => c.Type == ClaimTypes.Role)?.Value == "Admin")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
