using ClothingStoreModels.Dtos.Create;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;

namespace ClothingStoreAPI.Services.Interfaces
{
    public interface IProductReviewService
    {
        IEnumerable<ProductReviewDto> GetAll(int storeId, int productId);
        ProductReviewDto GetById(int storeId, int productId, int productReviewId);
        int Create(int storeId, int productId, CreateProductReviewDto dto);
        void Update(int storeId, int productId, int productReviewId, UpdateProductReviewDto dto);
        void DeleteAll(int storeId, int productId);
        void Delete(int storeId, int productId, int productReviewId);
    }
}
