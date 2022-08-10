using ClothingStoreAPI.Entities;
using ClothingStoreModels.Dtos.Create;

namespace ClothingStoreAPI.Services.Interfaces
{
    public interface IBasketService
    {
        void AddToBasket(CreateOrderDto dto, int productId);
        Basket GetExistingUserBasket();

    }
}
