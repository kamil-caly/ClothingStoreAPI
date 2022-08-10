namespace ClothingStoreAPI.Services.Interfaces
{
    public interface IOrderService
    {
        void AddOrder(int storeId, int productId, int quantity);
        void DeleteOrder(int orderId);
    }
}
