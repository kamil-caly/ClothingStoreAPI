using ClothingStoreAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ClothingStoreAPI.Authorization
{
    public class StoreResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, ClothingStore>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, ClothingStore store)
        {
            if (requirement.ResourceOperation == ResourceOperation.Read ||
                requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (store.CreatedById == int.Parse(userId) ||
                context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value == "Admin")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
