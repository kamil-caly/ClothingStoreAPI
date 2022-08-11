using ClothingStoreAPI.Entities;
using ClothingStoreModels.Dtos.Create;
using ClothingStoreModels.Dtos.Dispaly;

namespace ClothingStoreAPI.Services.Interfaces
{
    public interface IUserBasketService
    {
        void AddToBasket(CreateOrderDto dto, int productId);
        Basket GetExistingUserBasketForOrderService();
        int CreateBasket();
        BasketDto GetBasketDto();
        void DeleteBasket();
    }
}
