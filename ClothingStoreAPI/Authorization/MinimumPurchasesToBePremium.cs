using Microsoft.AspNetCore.Authorization;

namespace ClothingStoreAPI.Authorization
{
    public class MinimumPurchasesToBePremium : IAuthorizationRequirement
    {
        public MinimumPurchasesToBePremium(int minimumPurchases)
        {
            MinimumPurchases = minimumPurchases;
        }
        public int MinimumPurchases { get; }
    }
}
