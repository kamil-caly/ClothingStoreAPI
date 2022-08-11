using AutoMapper;
using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Exceptions;
using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos.Dispaly;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreAPI.Services
{
    public class BasketService : IBasketService
    {
        private readonly ClothingStoreDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<BasketService> logger;

        public BasketService(ClothingStoreDbContext dbContext, IMapper mapper,
            ILogger<BasketService> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        public void Delete(int basketId)
        {
            logger.LogError($"Clothing Store with id: {basketId} DELETE ACTION invoked");

            var basket = this.GetBasket(basketId);

            dbContext.Baskets.Remove(basket);
            dbContext.SaveChanges();
        }

        public void DeleteAll()
        {
            logger.LogError($"All Baskets DELETE ACTION invoked");

            var baskets = dbContext
                .Baskets
                .ToList();

            if (baskets is null)
            {
                throw new NotFoundAnyItemException("Not found any basket.");
            }

            dbContext.Baskets.RemoveRange(baskets);
            dbContext.SaveChanges();
        }

        public BasketDto Get(int basketId)
        {
            var basket = dbContext
                .Baskets
                .Include(b => b.Orders)
                .FirstOrDefault(b => b.Id == basketId);

            if (basket is null)
            {
                throw new NotFoundException($"Not found a basket with Id: {basketId}");
            }

            var basketDto = mapper.Map<BasketDto>(basket);
            return basketDto;
        }

        public IEnumerable<BasketDto> GetAll()
        {
            var baskets = dbContext
                .Baskets
                .Include(b => b.Orders)
                .ToList();

            if (baskets is null)
            {
                throw new NotFoundAnyItemException("Not found any basket.");
            }

            var basketDtos = mapper.Map<List<BasketDto>>(baskets);
            return basketDtos;
        }

        public Basket GetBasket(int basketId)
        {
            var basket = dbContext
                .Baskets
                .FirstOrDefault(b => b.Id == basketId);

            if (basket is null)
            {
                throw new NotFoundException($"Not found a basket with Id: {basketId}");
            }

            return basket;
        }
    }
}
