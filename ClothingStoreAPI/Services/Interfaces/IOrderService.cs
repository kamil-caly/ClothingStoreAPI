using ClothingStoreModels.Dtos.Dispaly;

namespace ClothingStoreAPI.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetAll(int basketId);
        OrderDto GetOrder(int basketId, int orderId);
        void DeleteAllOrders(int basketId);
        void DeleteOrder(int basketId, int orderId);
    }
}
