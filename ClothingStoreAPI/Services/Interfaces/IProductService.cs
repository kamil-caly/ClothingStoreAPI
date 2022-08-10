using ClothingStoreAPI.Entities;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;

namespace ClothingStoreAPI.Services.Interfaces
{
    public interface IProductService
    {
        PageResult<ProductDto> GetAll(int storeId, HttpQuery query);
        ProductDto GetById(int storeId, int productId);
        int Create(int storeId, CreateProductDto dto);

        void Update(int storeId, int productId, UpdateProductDto dto);
        void DeleteAll(int storeId);
        void Delete(int storeId, int productId);
        Product GetProductById(int productId, int storeId);
    }
}
