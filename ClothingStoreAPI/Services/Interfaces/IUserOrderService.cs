namespace ClothingStoreAPI.Services.Interfaces
{
    public interface IUserOrderService
    {
        void AddOrder(int storeId, int productId, int quantity);
        void DeleteOrder(int orderId);
        void BuyOrder(int orderId);
        void UpdateOrderQuantity(int orderId, int quantity);
    }
}
