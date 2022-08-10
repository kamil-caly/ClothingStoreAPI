using System.Security.Claims;

namespace ClothingStoreAPI.Services.Interfaces
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }
}
