using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;

namespace ClothingStoreAPI.Services.Interfaces
{
    public interface IStoreReviewService
    {
        IEnumerable<StoreReviewDto> GetAll(int storeId);
        StoreReviewDto GetById(int storeId, int reviewId);
        int Create(int storeId, CreateStoreReviewDto dto);

        void Update(int storeId, int reviewId, UpdateStoreReviewDto dto);
        void DeleteAll(int storeId);
        void Delete(int storeId, int reviewId);
    }
}
