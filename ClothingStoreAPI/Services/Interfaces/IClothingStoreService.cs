using ClothingStoreAPI.Entities;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;

namespace ClothingStoreAPI.Services.Interfaces
{
    public interface IClothingStoreService
    {
        int Create(CreateClothingStoreDto dto);
        PageResult<ClothingStoreDto> GetAll(HttpQuery query);
        ClothingStoreDto GetById(int id);
        public void Delete(int id);
        public void Update(UpdateClothingStoreDto dto, int id);
        ClothingStore GetStoreFromDb(int id);
    }
}
