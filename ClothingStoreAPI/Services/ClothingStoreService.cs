using AutoMapper;
using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Exceptions;
using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreAPI.Services
{
    public class ClothingStoreService : IClothingStoreService
    {
        private readonly ClothingStoreDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<ClothingStoreService> logger;

        public ClothingStoreService(ClothingStoreDbContext dbContext, IMapper mapper, ILogger<ClothingStoreService> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
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
            logger.LogError($"Clothing Store with id: {id} DELETE ACTION invoked");

            var store = this.GetStoreFromDb(id);
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
                throw new NotFoundAnyItemException("Cannot found any clothing stores");
            }

            var result = mapper.Map<List<ClothingStoreDto>>(stores);
            return result;
        }

        public ClothingStoreDto GetById(int id)
        {
            var store = this.GetStoreFromDb(id);
            var restult = mapper.Map<ClothingStoreDto>(store);
            return restult;
        }

        public void Update(UpdateClothingStoreDto dto, int id)
        {
            var store = this.GetStoreFromDb(id);

            store = mapper.Map(dto, store);
            dbContext.SaveChanges();
        }

        public ClothingStore GetStoreFromDb(int id)
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
                throw new NotFoundException("Store not found");
            }

            return store;
        }
    }
}
