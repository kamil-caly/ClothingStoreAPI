using AutoMapper;
using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Exceptions;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreAPI.Services
{
    public interface IClothingStoreService
    {
        int Create(CreateClothingStoreDto dto);
        IEnumerable<ClothingStoreDto> GetAll();
        ClothingStoreDto GetById(int id);
        public void Delete(int id);
        public void Update(UpdateClothingStoreDto dto, int id);
    }
    public class ClothingStoreService : IClothingStoreService
    {
        private readonly ClothingStoreDbContext dbContext;
        private readonly IMapper mapper;

        public ClothingStoreService(ClothingStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public int Create(CreateClothingStoreDto dto)
        {
            var store = mapper.Map<ClothingStore>(dto);
            dbContext.ClothingStores.Add(store);
            dbContext.SaveChanges();

            return store.Id;
        }

        public void Delete(int id)
        {
            var store = this.GetStoreById(id);
            dbContext.ClothingStores.Remove(store);
            dbContext.SaveChanges();
        }

        public IEnumerable<ClothingStoreDto> GetAll()
        {
            var stores = dbContext
                .ClothingStores
                .Include(s => s.Address)
                .Include(s => s.Owner)
                .Include(s => s.StoreReviews)
                .Include(s => s.Products)
                .ToList();

            if(stores is null)
            {
                throw new NotFoundAnyStoresException("Cannot found any clothing stores");
            }

            var result = mapper.Map<List<ClothingStoreDto>>(stores);
            return result;
        }

        public ClothingStoreDto GetById(int id)
        {
            var store = this.GetStoreById(id);
            var restult = mapper.Map<ClothingStoreDto>(store);
            return restult;
        }

        public void Update(UpdateClothingStoreDto dto, int id)
        {
            var store = this.GetStoreById(id);

            store = mapper.Map(dto, store);
            dbContext.SaveChanges();
        }

        private ClothingStore GetStoreById(int id)
        {
            var store = dbContext
                .ClothingStores
                .Include(s => s.Address)
                .Include(s => s.Owner)
                .Include(s => s.StoreReviews)
                .Include(s => s.Products)
                .FirstOrDefault(s => s.Id == id);

            if (store is null)
            {
                throw new NotFoundStoreException("Store not found");
            }

            return store;
        }
    }
}
