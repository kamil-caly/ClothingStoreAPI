using Microsoft.AspNetCore.Authorization;

namespace ClothingStoreAPI.Authorization
{
    public class MinimumStoreCreated : IAuthorizationRequirement
    {
        public int MinimumStore { get; }
        public MinimumStoreCreated(int minimumStore)
        {
            MinimumStore = minimumStore;
        }
    }
}
