using ClothingStoreAPI.Entities;
using ClothingStoreModels.Dtos.Dispaly;

namespace ClothingStoreAPI.Services.Interfaces
{
    public interface IBasketService
    {
        IEnumerable<BasketDto> GetAll();
        BasketDto Get(int basketId);
        void DeleteAll();
        void Delete(int basketId);
        Basket GetBasket(int basketId);
    }
}
